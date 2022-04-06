using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuarioApi.Data.Dtos.User;
using UsuarioApi.Data.Requests;
using UsuarioApi.Models;

namespace UsuarioApi.Service
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(
            IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }
 
        public Result UserCreate(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);

            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);

            Task<IdentityResult> resIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);

            if (resIdentity.Result.Succeeded) 
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;

                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new[] { userIdentity.Email },
                    "Link de ativação", userIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code);
            }

            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result UserActive(UserActiveRequest req)
        {
            var identityUser = _userManager
                .Users.FirstOrDefault(u => u.Id == req.IdUser);

            var identityRes = _userManager
                .ConfirmEmailAsync(identityUser, req.CodigoDeAtivacao).Result;

            if (identityRes.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
