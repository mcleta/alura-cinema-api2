using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuarioApi.Data.Requests;
using UsuarioApi.Models;

namespace UsuarioApi.Service
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager,
            TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogarUser(LoginRequest req)
        {
            var resIdentity = _signInManager
                .PasswordSignInAsync(req.UserName, req.Password, false, false);
            if (resIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario =>
                    usuario.NormalizedUserName == req.UserName.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }
    }
}
