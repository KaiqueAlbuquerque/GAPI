using ApiAlteraPDF.Autenticacao;
using ApiQrCode.ViewModel;
using Application.Interfaces;
using Dominio.Entidades;
using RestSharp;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiQrCode.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        private readonly IAppUsuario _appUsuario;

        public UsuarioController(IAppUsuario appUsuario)
        {
            _appUsuario = appUsuario;
        }

        [HttpPost]
        public IHttpActionResult CadastraUsuario([FromBody]Usuario usuario)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 3);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        _appUsuario.CadastraUsuario(usuario);
                        return Ok(usuario);
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado.StatusCode, "");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult ConsultaUsuarios(int? idCliente, int idApiPertencente, int pagina)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 3);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        UsuariosViewModel retorno = new UsuariosViewModel();
                        retorno.Usuarios = _appUsuario.ConsultaUsuarios(idCliente, idApiPertencente, pagina);
                        retorno.QuantidadePaginas = _appUsuario.QuantidadePaginas();

                        return Ok(retorno);
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado.StatusCode, "");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult ConsultaUsuario(int idUsuario)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 3);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(_appUsuario.ConsultaUsuario(idUsuario));
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado.StatusCode, "");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult ConsultaUsuarioIdLogin(int idLogin, int idApiPertencente)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 3);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(_appUsuario.ConsultaUsuarioIdLogin(idLogin, idApiPertencente));
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado.StatusCode, "");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult AlteraUsuario([FromBody]Usuario usuario)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 3);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        _appUsuario.AlteraUsuario(usuario);
                        return Ok(usuario);
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado.StatusCode, "");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "");
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
