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
    public class ClienteController : ApiController
    {
        private readonly IAppCliente _clienteApp;

        public ClienteController(IAppCliente clienteApp)
        {
            _clienteApp = clienteApp;
        }

        [HttpPost]
        public IHttpActionResult CadastraCliente([FromBody]Cliente cliente)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if(Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 3);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        _clienteApp.CadastraCliente(cliente);
                        return Ok(cliente);
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
        public IHttpActionResult ConsultaClientes(int idApiPertencente, int pagina)
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
                        ClientesViewModel retorno = new ClientesViewModel();
                        retorno.Clientes = _clienteApp.ConsultaClientes(idApiPertencente, pagina);
                        retorno.QuantidadePaginas = _clienteApp.QuantidadePaginas(idApiPertencente);

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
        public IHttpActionResult ConsultaClientes(int idApiPertencente)
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
                        return Ok(_clienteApp.ConsultaClientes(idApiPertencente));
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
        public IHttpActionResult ConsultaCliente(int idCliente)
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
                        return Ok(_clienteApp.ConsultaCliente(idCliente));
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
        public IHttpActionResult AlteraCliente([FromBody]Cliente cliente)
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
                        _clienteApp.AlteraCliente(cliente);
                        return Ok(cliente);
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
