using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuarioApi.Models
{
    internal class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto, int userId, string code)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));
            Assunto = assunto;
            Conteudo = $"https://localhost:6001/active?IdUser={userId}&CodigoDeAtivacao={code}";
        }
    }
}