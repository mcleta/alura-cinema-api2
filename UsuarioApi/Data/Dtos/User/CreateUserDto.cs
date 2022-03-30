using System.ComponentModel.DataAnnotations;

namespace UsuarioApi.Data.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="As senhas tem de ser identicas!!!")]
        public string RePassword { get; set; }
    }
}
