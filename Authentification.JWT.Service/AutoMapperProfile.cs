using Authentification.JWT.DAL.Models;
using AutoMapper;
using Authentification.JWT.Service.DTOS;


namespace Authentification.JWT.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,LoginModel>().ReverseMap();
        }
    }
}
