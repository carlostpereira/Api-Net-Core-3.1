using App.Domain.Commands.Response;
using App.Domain.Entities;
using AutoMapper;

namespace App.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
