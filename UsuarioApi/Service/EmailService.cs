using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuarioApi.Models;

namespace UsuarioApi.Service
{
    public class EmailService
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void EnviarEmail(
            string[] destinatario, string assunto, 
            int userId, string code)
        {
            Mensagem msm = new Mensagem(destinatario, assunto, userId, code);
            var msmDeEmail = CriarCorpoEmail(msm);
            Enviar(msmDeEmail);
        }

        private void Enviar(MimeMessage msmDeEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config.GetValue<string>("EmailSettings:SmtpServer"),
                        _config.GetValue<int>("EmailSettings:Port"), true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_config.GetValue<string>("EmailSettings:From"),
                        _config.GetValue<string>("EmailSettings:Password"));
                    client.Send(msmDeEmail);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriarCorpoEmail(Mensagem msm)
        {
            var msmDeEmail = new MimeMessage();
            msmDeEmail.From.Add(new MailboxAddress(
                _config.GetValue<string>("EmailSettings:From")));
            msmDeEmail.To.AddRange(msm.Destinatario);
            msmDeEmail.Subject = msm.Assunto;
            msmDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = msm.Conteudo
            };

            return msmDeEmail;
        }
    }
}
