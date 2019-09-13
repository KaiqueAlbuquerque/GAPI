using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Jwt.Filters;
using WebApi.Jwt.Gerenciamento;
using WebApi.Jwt.ViewModel;

namespace WebApi.Jwt.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly IUsuarioIntermediador _usuarioIntermediador;

        public LoginController(IUsuarioIntermediador usuarioIntermediador)
        {
            _usuarioIntermediador = usuarioIntermediador;
        }

        //Realiza login. Sitemas que iteragem com as APIs sempre utilizam este método pois sempre fazem login
        [AllowAnonymous]
        public string Post([FromBody] LoginViewModel login)
        {
            var senha = Criptografia.Encrypt(login.Senha);
            HttpStatusCode retorno = _usuarioIntermediador.BuscaUsuario(login.Email.Trim().ToLower(), senha, login.AcessoId);

            if (retorno == HttpStatusCode.OK)
            {
                return JwtManager.GenerateToken(login.Email.Trim().ToLower());
            }
            else
            {
                throw new HttpResponseException(retorno);
            }                      
        }

        //Utilizado no GAPI para validar se o usuário está logado
        [JwtAuthentication]
        public IHttpActionResult Get(int acesso)
        {
            JwtAuthenticationAttribute jwt = new JwtAuthenticationAttribute();

            var email = jwt.RetornaEmailToken(Request.Headers.Authorization.Parameter);

            if(!string.IsNullOrEmpty(email))
            {
                Usuario usuario = _usuarioIntermediador.GetAll().Where(u => u.Email == email).FirstOrDefault();

                if(usuario != null)
                {
                    if(usuario.Acessos != null)
                    {
                        foreach (var a in usuario.Acessos)
                        {
                            if (a.AcessoId == acesso && a.Ativo)
                            {
                                return Ok(usuario);
                            }
                        }
                    }

                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
