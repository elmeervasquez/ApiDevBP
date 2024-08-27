using ApiDevBP.Common;
using ApiDevBP.Models;
using AutoMapper;

namespace ApiDevBP.Entities
{
    public class UserMapper : Profile, IMapperProfile
    {
        public UserMapper()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}