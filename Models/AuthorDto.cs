namespace BookEntityFramework.Models
{
    public class AuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; }
        public string Birthdate { get; set; }
        public string? Country { get; set; }
    }

    public class UpdateAuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
    }

}
