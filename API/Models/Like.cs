using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaWeb.Models
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }=DateTime.Now;

        public bool IsLike { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("SocialMediaPost")]
        public int PostId { get; set; }
        public SocialMediaPost? Post { get; set; }
     
    }
}
