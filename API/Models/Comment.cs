using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaWeb.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string? Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get;set; }
        public User? User { get; set; }

        [ForeignKey("SocialMediaPost")]
        public int PostId { get; set; }

        public SocialMediaPost? SocialMediaPost { get; set; }
    }
}
