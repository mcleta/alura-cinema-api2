using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using UsuarioApi.Data.Requests;

namespace UsuarioApi.Service
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LoginService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LogarUser(LoginRequest req)
        {
            var resIdentity = _signInManager.
                PasswordSignInAsync(req.UserName, req.Password, false, false);

            if (resIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Login falhou!!! Usuário ou senha incorretos.");
        }
    }
}
