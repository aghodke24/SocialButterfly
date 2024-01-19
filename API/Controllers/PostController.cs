using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;
using SocialMediaWeb.Services.Classes;
using SocialMediaWeb.Services.Interfaces;

namespace SocialMediaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ISocialMediaPost _post;
        private readonly ICommentService _comment;

        public PostController(ISocialMediaPost post, ICommentService comment)
        {
            _post = post;
            _comment = comment;
        }

        [HttpPost]
        public async Task<ActionResult<SocialMediaPost>> CreatePost([FromForm] PostCreateDto postCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _post.CreatePostAsync(postCreateDto);

            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            var comments = await _comment.GetCommentsAsync();

            if (comments == null || !comments.Any())
            {
                return NoContent();
            }

            return Ok(comments);
        }

       /* [HttpGet("GetAllPosts")]
        public async Task<ActionResult<Comment>> GetComment()
        {
            var post = await _post.GetAllPostsAsync();

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }*/

        [HttpPost("Comments")]
        public async Task<ActionResult<Comment>> CreateComment(CreateCommentDto commentCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _comment.CreateCommentAsync(commentCreateDto);

            return Ok( new { id = comment.Id,comment });
        }

        [HttpGet("UserPost")]
        public async Task<ActionResult<IEnumerable<SocialMediaPost>>> UserPost()
        {
            try
            {
                var post = await _post.AllPost();
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SocialMediaPost>>> GetSingleUser(int id)
        {
            try
            {
                var post = await _post.GetUserpost(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
