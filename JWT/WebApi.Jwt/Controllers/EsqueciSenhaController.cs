using Dominio.Interfaces.Intermediadores;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Jwt.Gerenciamento;

namespace WebApi.Jwt.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EsqueciSenhaController : ApiController
    {
        private readonly IUsuarioIntermediador _usuarioIntermediador;

        public EsqueciSenhaController(IUsuarioIntermediador usuarioIntermediador)
        {
            _usuarioIntermediador = usuarioIntermediador;
        }

        //Se o usuário existe e tem acesso ao GAPI, receberá em seu e-mail a senha
        [AllowAnonymous]
        public bool Post([FromBody] string email)
        {
            var usuario = _usuarioIntermediador.GetAll().Where(u => u.Ativo && u.Email == email.ToLower().Trim()).FirstOrDefault();

            if(usuario != null)
            {
                foreach(var acesso in usuario.Acessos)
                {
                    if(acesso.AcessoId == 3)
                    {
                        string Body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["modeloEmail"]));
                        Body = Body.Replace("#Mensagem#", "<p style='font: 12px Verdana, Geneva, sans-serif; color: #666; line-height:25px;'>Olá " + usuario.Nome + ",<br /><br />Sua senha é: " + Criptografia.Decrypt(usuario.Senha) + "</p><p style='font: 10px Verdana, Geneva, sans-serif; color: #666; line-height:25px;'>Não divulgue sua senha. Ela é pessoal e intransferivel.</p>");

                        SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["porta"]));
                        NetworkCredential credenciais = new NetworkCredential(ConfigurationManager.AppSettings["emailUsuario"], ConfigurationManager.AppSettings["emailSenha"], "");
                        client.Credentials = credenciais;

                        MailAddress de = new MailAddress(ConfigurationManager.AppSettings["emailRemetente"]);
                        MailAddress para = new MailAddress(usuario.Email);

                        MailMessage mensagem = new MailMessage(de, para);
                        mensagem.IsBodyHtml = true;

                        mensagem.Subject = "GAPI - Nova Senha";
                        mensagem.Body = Body;

                        client.Send(mensagem);
                    }
                }
            }

            return true;
        }
    }
}
