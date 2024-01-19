using AutoMapper;
using SocialMediaWeb.Models;
using System.Net;

namespace SocialMediaWeb.Dtos
{
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User,UserResponseDto>();
            CreateMap<User, ResponseDto>();

            CreateMap<UserUpdateDto, User>();

            CreateMap<CreateCommentDto, Comment>();
        }
        }
    }

