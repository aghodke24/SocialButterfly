using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaWeb.Models
{
    public class SocialMediaPost
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string? Descriprion { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get;set; }

        public DateTime? CreatedAt { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Like>? Likes { get; set; }



    }
}
