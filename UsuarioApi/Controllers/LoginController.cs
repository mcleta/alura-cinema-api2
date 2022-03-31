using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using UsuarioApi.Data.Requests;
using UsuarioApi.Service;

namespace UsuarioApi.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult UserCreate(LoginRequest req)
        {
            Result res = _loginService.LogarUser(req);

            if (res.IsFailed) return Unauthorized();

            Console.WriteLine("Login efetuado com sucesso");
            return Ok();
        }
    }
}
