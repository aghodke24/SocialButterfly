using SocialMediaWeb.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaWeb.Dtos
{
    public class CreateCommentDto
    {

        public string? Text { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

    }
}
