using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;
using SocialMediaWeb.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaWeb.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, IMapper mapper, IConfiguration config, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserResponseDto> LoginAsync(UserLoginDto loginDto)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }


            var userResponseDto = _mapper.Map<UserResponseDto>(user);
                var jwt = GenerateJwt(user);
                userResponseDto.Token = jwt;

            return userResponseDto;
        }
       

        public async Task RegisterAsync(UserRegistrationDto model)
        {

            /*string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string fileExtension = Path.GetExtension(model.ImageFile.FileName);
            string uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{fileExtension}";
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", uniqueFileName);
            await System.IO.File.WriteAllTextAsync(filePath, model.Image);*/

            var httpContext = _httpContextAccessor.HttpContext;
            string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string fileExtension = Path.GetExtension(model.ImageFile.FileName);
            string uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{fileExtension}";
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", uniqueFileName);
            string imageUrl = $"{baseUrl}/Images/{uniqueFileName}";
            await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

            var user = _mapper.Map<User>(model);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, BCrypt.Net.BCrypt.GenerateSalt());
            user.ImagePath = imageUrl; // Save image URL to database

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                _ = e.Message;
            }
        }

        public async Task<User> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == userUpdateDto.Id);
            if (user == null)
            {
                throw new Exception($"User with id {userUpdateDto.Id} not found");
            }

            user.Name = userUpdateDto.Name ?? user.Name;
            user.Dob = userUpdateDto.Dob;
            user.Email = userUpdateDto.Email ?? user.Email;
            user.Information = userUpdateDto.Information ?? user.Information;

            if (userUpdateDto.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    DeleteImage(user.ImagePath);
                }
                var imagePath = await SaveImageAsync(userUpdateDto.ImageFile);
                user.ImagePath = imagePath;
            }
                await _context.SaveChangesAsync();
                return user;
            
        }

        private async Task<string> SaveImageAsync(IFormFile imagePath)
        {
            /*string fileName = Path.GetFileNameWithoutExtension(imagePath.FileName);
            string fileExtension = Path.GetExtension(imagePath.FileName);
            string uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{fileExtension}";

            // Save the image to the wwwroot/Images folder
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await imagePath.CopyToAsync(fileStream);

            // Return the image URL with forward slashes
            var httpContext = _httpContextAccessor.HttpContext;
            string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            string imageUrl = $"{baseUrl}/Images/{uniqueFileName}";
            return imageUrl;*/

            var fileName = $"{DateTime.Now.Ticks}_{imagePath.FileName}";
            var filePath = Path.Combine("wwwroot", "Images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imagePath.CopyToAsync(fileStream);
            }

            // Get the base URL of the application
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            // Return the image URL with forward slashes
            return $"{baseUrl}/Images/{fileName}";

        }

        
        private void DeleteImage(string imagePath)
        {
            var filePath = Path.Combine("wwwroot", imagePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
       

        private string GenerateJwt(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            _ = int.TryParse(_config["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);


            var token = new JwtSecurityToken(
            issuer: _config["JWT:ValidIssuer"],
            audience: _config["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: claims,
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<User> FindById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new NullReferenceException("User cannot be found");
            }

            return user;
        }

        public async Task<List<ResponseDto>> GetUserAsync()
        {
            var user = await _context.Users.ToListAsync();

            return _mapper.Map<List<User>, List<ResponseDto>>(user);

        }
    }
}
