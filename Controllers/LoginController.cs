using BookEntityFramework.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUser _loginService;
        public LoginController(IUser loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("/Register")]
        public ActionResult RegisterUser(UserDto dto)
        {
            _loginService.Register(dto);
            return Ok();
        }
        [HttpPost]
        [Route("/login")]
        public ActionResult LoginUser(UserDto dto)
        {
            return Ok(_loginService.Login(dto));
        }
    }
}
