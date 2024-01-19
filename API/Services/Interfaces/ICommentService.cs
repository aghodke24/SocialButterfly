using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;

namespace SocialMediaWeb.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(CreateCommentDto commentCreateDto);
        Task<List<Comment>> GetCommentAsync(int postId);
        Task<List<Comment>> GetCommentsAsync();
    }
}
