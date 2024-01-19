using System.ComponentModel.DataAnnotations;

namespace SocialMediaWeb.Dtos
{
    public class UserRegistrationDto
    {
        public string? Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Dob { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Role { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? Image { get; set; }

        public string? Information { get; set; }


    }
}
