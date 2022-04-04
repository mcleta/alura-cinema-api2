using System.ComponentModel.DataAnnotations;

namespace UsuarioApi.Data.Requests
{
    public class UserActiveRequest
    {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}