using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;

namespace SocialMediaWeb.Services.Interfaces
{
    public interface ISocialMediaPost
    {
        Task<SocialMediaPost> CreatePostAsync(PostCreateDto createDto);

/*        Task<List<SocialMediaPost>> GetAllPostsAsync();*/

        Task<List<SocialMediaPost>> AllPost();

        Task<List<SocialMediaPost>> GetUserpost(int id);

    }
}
