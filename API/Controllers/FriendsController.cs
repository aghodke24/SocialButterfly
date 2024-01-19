using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;
using SocialMediaWeb.Services.Interfaces;

namespace SocialMediaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friend;

        public FriendsController(IFriendService friend)
        {
            _friend = friend;
        }


        [HttpPost]
        public async Task<ActionResult<FriendDto>> SendfriendRequest(FriendDto friends)
        {
            try
            {
                var friend = await _friend.SendFriendRequest(friends);
                return Ok(friend);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{senderId}")]
        public async Task<ActionResult<FriendDto>> AcceptFriendRequest(int senderId)
        {
            try
            {
                var friend = await _friend.AcceptFriendRequest(senderId);
                return Ok(friend);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/friends")]
        public async Task<ActionResult<FriendDto>> GetAllFriends(int friendId)
        {
            try
            {
                var friend = await _friend.GetFriends(friendId);
                return Ok(friend);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{receiver}/requests")]
        public async Task<ActionResult<FriendDto>> Requests(int receiver)
        {
            try
            {
                var friend = await _friend.GetRequest(receiver);
                return Ok(friend);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{receiver}/requestuser")]
        public async Task<ActionResult<FriendDto>> RequestUser(int receiver)
        {
            try
            {
                var friend = await _friend.GetUserRequest(receiver);
                return Ok(friend);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{receiver}/myfriends")]
        public async Task<ActionResult<FriendDto>> MyFriends(int receiver)
        {
            try
            {
                var friend = await _friend.GetMyFriends(receiver);
                return Ok(friend);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{receiver}/friendscount")]
        public async Task<ActionResult<FriendDto>> MyFriendsCount(int receiver)
        {
            try
            {
                var friend = await _friend.FriendsCount(receiver);
                return Ok(friend);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
