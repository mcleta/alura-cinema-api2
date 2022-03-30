using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuarioApi.Data.Dtos.User;
using UsuarioApi.Models;

namespace UsuarioApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, IdentityUser<int>>();
        }
    }
}
