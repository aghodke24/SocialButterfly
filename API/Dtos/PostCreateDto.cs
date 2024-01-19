namespace SocialMediaWeb.Dtos
{
    public class PostCreateDto
    {
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
    }
}

