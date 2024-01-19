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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService service) { 
        
            _service = service;
        }


        [HttpPost]
        public async Task<ActionResult<Comment>> AddCommentToPost(CreateCommentDto commentDto)
        {
            var comment = await _service.CreateCommentAsync(commentDto);
            return Ok(comment);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<FriendDto>> MyFriends(int postId)
        {
            try
            {
                var comments = await _service.GetCommentAsync(postId);
                return Ok(comments);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
