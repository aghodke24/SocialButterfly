using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;
using SocialMediaWeb.Services.Interfaces;

namespace SocialMediaWeb.Services.Classes
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentService(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Comment> CreateCommentAsync(CreateCommentDto commentCreateDto)
        {
           
                var comment = _mapper.Map<Comment>(commentCreateDto);

            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();


            return comment;

        }

        public async Task<List<Comment>> GetCommentAsync(int postId)
        {
            var comments = await _dbContext.Comments.Include(c => c.User).Where(c => c.PostId == postId).ToListAsync();

            return comments;
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }


    }
}
