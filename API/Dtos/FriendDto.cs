using SocialMediaWeb.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaWeb.Dtos
{
    public class FriendDto
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime AcceptedAt { get; set; }
    }
}
