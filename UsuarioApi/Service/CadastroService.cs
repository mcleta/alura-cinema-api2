using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UsuarioApi.Data.Dtos.User;
using UsuarioApi.Models;

namespace UsuarioApi.Service
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result UserCreate(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);

            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);

            Task<IdentityResult> resIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);

            if (resIdentity.Result.Succeeded) return Result.Ok();

            return Result.Fail("Falha ao cadastrar usuário");
        }
    }
}
