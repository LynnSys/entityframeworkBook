using BookEntityFramework.AppSetttings;
using BookEntityFramework.Interfaces;
using EBook.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookEntityFramework.Repository
{
    public class LoginRepository : IUser
    {
        private readonly LynnContext _context;
        private readonly IConfiguration _configurations;
        private readonly JwtClaimDetails _jwtClaimDetails;

        public LoginRepository(IConfiguration configurations, IOptions<JwtClaimDetails> jwtDetails, LynnContext context)
        {
            _configurations = configurations;
            _jwtClaimDetails = jwtDetails.Value;
            _context = context;
        }

        public string Login(UserDto loginDto)
        {

            string token = CreateToken(loginDto);
            //return Ok(token);

            return token;
        }

        private string CreateToken(UserDto user)
        {
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            try
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    throw new ArgumentException("Invalid username or password");
                }
                string hashedPassword = "";
                string role = "";
                //usp_storedprodname
                var storedprod = "PassVerification";
                SqlCommand command = new SqlCommand(storedprod, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;

                hashedPassword = command.ExecuteScalar() as string;

                if (hashedPassword != null && BCrypt.Net.BCrypt.EnhancedVerify(user.Password, hashedPassword))
                {
                    string roleQuery = "GetRole";
                    SqlCommand roleCommand = new SqlCommand(roleQuery, connection);
                    roleCommand.CommandType = CommandType.StoredProcedure;
                    roleCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.Username;
                    roleCommand.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = hashedPassword;
                    var result = roleCommand.ExecuteScalar();
                    if (result != null)
                    {
                        role = Convert.ToString(result);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid username or password");
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtClaimDetails.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claim = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
                };

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtClaimDetails.Issuer,
                    audience: _jwtClaimDetails.Audience,
                    claims: claim,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signinCredentials
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public string Register(UserDto registerDto)
        {


            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(registerDto.Password, 13);
            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                Role = Roles.User.ToString()
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return "ok";

        }

    }
}
