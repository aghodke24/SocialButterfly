
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWeb.Dtos
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? Token { get; set; }

        public string? ImagePath { get; set; }
    }
}
