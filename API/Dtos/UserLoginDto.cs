using System.ComponentModel.DataAnnotations;

namespace SocialMediaWeb.Dtos
{
    public class UserLoginDto
    {

        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
