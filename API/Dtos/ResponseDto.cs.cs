using System.ComponentModel.DataAnnotations;

namespace SocialMediaWeb.Dtos
{
    public class ResponseDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime Dob { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? ImagePath { get; set; }
    }
}
