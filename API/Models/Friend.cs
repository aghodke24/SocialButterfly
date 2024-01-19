using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaWeb.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime AcceptedAt { get; set; } = DateTime.Now;

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
