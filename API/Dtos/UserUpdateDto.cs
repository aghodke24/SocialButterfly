using System.ComponentModel.DataAnnotations;

namespace SocialMediaWeb.Dtos
{
    public class UserUpdateDto
    {
     
        public int Id { get; set; }
        public string? Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Dob { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? Information { get; set; }

        public string? Image { get; set; }
        

    }
}
