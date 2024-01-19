using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;
using SocialMediaWeb.Services.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace SocialMediaWeb.Services.Classes
{
    public class SocialMedia:ISocialMediaPost
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SocialMedia(ApplicationDbContext context, IMapper mapper,IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SocialMediaPost> CreatePostAsync(PostCreateDto createDto)
        {
            var post = new SocialMediaPost
            {
                Descriprion = createDto.Description,
                UserId = createDto.UserId,
                CreatedAt=createDto.CreatedAt
            };

            if (createDto.ImageFile != null && createDto.ImageFile.Length > 0)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
                string fileName = Path.GetFileNameWithoutExtension(createDto.ImageFile.FileName);
                string fileExtension = Path.GetExtension(createDto.ImageFile.FileName);
                string uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{fileExtension}";
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "posts", uniqueFileName);
                string imageUrl = $"{baseUrl}/posts/{uniqueFileName}";
                await createDto.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                post.ImagePath = imageUrl; // Save image URL to database
                // Set the ImagePath property of the post to the path of the saved image
            }
            post.CreatedAt = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

       /* public async Task<List<SocialMediaPost>> GetAllPostsAsync()
        {

            var posts = await (from post in _context.Posts
                   join users in _context.Users on post.UserId equals users.Id
                   select new
                   {
                       post.Id,
                       post.ImagePath,
                       post.UserId,
                       UsersImagePath=users.ImagePath,
                       post.Descriprion

                   }).ToListAsync();

            var socialMediaPosts = posts.Select(p => new SocialMediaPost
            {
                Id = p.Id,
                ImagePath = p.ImagePath,
                UserId = p.UserId,
                UserImagePath = p.UsersImagePath,
                Descriprion = p.Descriprion,
               
            }).ToList();

            return socialMediaPosts;
        }*/

        public async Task<List<SocialMediaPost>> AllPost()
        {

            var posts = await _context.Posts.Include(p=>p.User).ToListAsync();
            return posts;
        }

        public async Task<List<SocialMediaPost>> GetUserpost(int id)
        {

            var posts = await _context.Posts.Include(p => p.User).Where(e=>e.UserId==id).ToListAsync();
            return posts;
        }

    }
}
