using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;
using SocialMediaWeb.Services.Interfaces;

namespace SocialMediaWeb.Services.Classes
{
    public class FriendService:IFriendService
    {
        private readonly ApplicationDbContext _context;

        public FriendService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<FriendDto>> SendFriendRequest(FriendDto friendDto)
        {
            var friend = new Friend
            {
                SenderId = friendDto.SenderId,
                ReceiverId = friendDto.ReceiverId,
                IsAccepted = false,
                AcceptedAt = DateTime.UtcNow,
            };

            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();

            var friendDtoResponse = new FriendDto
            {
                Id = friend.Id,
                SenderId = (int)friend.SenderId,
                ReceiverId = (int)friend.ReceiverId,
                IsAccepted = friend.IsAccepted,
                AcceptedAt = friend.IsAccepted ? friend.AcceptedAt : DateTime.MinValue
            };

            return friendDtoResponse;
        }

        public async Task<ActionResult<FriendDto>> AcceptFriendRequest(int SenderId)
        {
            var friend = await _context.Friends.FirstOrDefaultAsync(u=>u.SenderId==SenderId);

            if (friend == null)
            {
                throw new Exception("Value Not found");
            }

            friend.IsAccepted = true;

            _context.Entry(friend).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var response = new FriendDto
            {
                Id = friend.Id,
                SenderId = (int)friend.SenderId!,
                ReceiverId = (int)friend.ReceiverId!,
                IsAccepted = friend.IsAccepted,
                AcceptedAt = friend.AcceptedAt
            };

            return response;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetFriends(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Senders)!.ThenInclude(f => f.Receiver)
                .Include(u => u.Receivers)!.ThenInclude(f => f.Sender)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var friends = user.Senders
        .Where(f => f.IsAccepted && f.Receiver != null)
        .Select(f => f.Receiver)
        .Union(user.Receivers
            .Where(f => f.IsAccepted && f.Sender != null)
            .Select(f => f.Sender))
        .Distinct()
        .ToList();

            return friends;
        }

        public async Task<ActionResult<int>> GetRequest(int receiverId)
        {
            var count = await _context.Friends
                    .CountAsync(f => f.ReceiverId == receiverId && !f.IsAccepted);

            return count;
        }

        public async Task<ActionResult<int>> FriendsCount(int receiverId)
        {
            var count = await _context.Friends
                    .CountAsync(f => f.ReceiverId == receiverId && f.IsAccepted== true);

            return count;
        }

        public async Task<ActionResult<List<RequestDto>>> GetUserRequest(int receiverId)
        {
            var friends = await (from u in _context.Users
                                 join f in _context.Friends on u.Id equals f.SenderId
                                 where f.ReceiverId == receiverId && f.IsAccepted == false
                                 select new RequestDto
                                 {
                                     ImagePath = u.ImagePath,
                                     Name = u.Name,
                                     Dob = u.Dob,
                                     Id = u.Id
                                 }).ToListAsync();

            return friends;
        }

        public async Task<ActionResult<List<RequestDto>>> GetMyFriends(int receiverId)
        {
            var friends = await (from u in _context.Users
                                 join f in _context.Friends on u.Id equals f.SenderId
                                 where f.ReceiverId == receiverId && f.IsAccepted == true
                                 select new RequestDto
                                 {
                                     ImagePath = u.ImagePath,
                                     Name = u.Name,
                                     Dob = u.Dob, 
                                     Id = u.Id
                                 }).ToListAsync();

            return friends;
        }
    }
}


