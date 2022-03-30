using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos.User;
using UsuarioApi.Service;

namespace UsuarioApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class CadrastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadrastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult UserCreate(CreateUserDto createDto)
        {
            Result res = _cadastroService.UserCreate(createDto);

            if (res.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
}
