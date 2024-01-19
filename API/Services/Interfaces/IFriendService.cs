using Microsoft.AspNetCore.Mvc;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;

namespace SocialMediaWeb.Services.Interfaces
{
    public interface IFriendService
    {
        Task<ActionResult<FriendDto>> SendFriendRequest(FriendDto friendDto);

        Task<ActionResult<FriendDto>> AcceptFriendRequest(int friendId);

        Task<ActionResult<IEnumerable<User>>> GetFriends(int userId);

        Task<ActionResult<int>> GetRequest(int receiverId);

        Task<ActionResult<List<RequestDto>>> GetUserRequest(int receiverId);

        Task<ActionResult<List<RequestDto>>> GetMyFriends(int receiverId);

        Task<ActionResult<int>> FriendsCount(int receiverId);
    }
}
