using App.Domain.Commands.Response;
using App.Domain.Entities;
using AutoMapper;

namespace App.Domain.Profiles
{
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<Provider, ProviderResponse>().ReverseMap();
        }
    }
}
