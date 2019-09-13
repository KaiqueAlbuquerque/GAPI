using ApiAlteraPDF.Autenticacao;
using ApiAlteraPDF.ViewModel;
using Dominio.Interfaces.Intermediadores;
using Dominio.Interfaces.ObjVal;
using Dominio.ObjVal;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiAlteraPDF.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QrCodeController : ApiController
    {
        private readonly IIntermediadorLog _logIntermediador;
        private readonly IIntermediadorUsuario _usuarioIntermediador;

        public QrCodeController(IIntermediadorLog logIntermediador, IIntermediadorUsuario usuarioIntermediador)
        {
            _logIntermediador = logIntermediador;
            _usuarioIntermediador = usuarioIntermediador;
        }

        [HttpPost]
        public IHttpActionResult GeraQrCodeToBack()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToBack(httpRequest.Params["login"], httpRequest.Params["senha"], 2);

                if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                {
                    string token = JsonConvert.DeserializeObject<string>(verificaSeEstaLogado.Content);

                    IGeraArquivo geraArquivo = new QrCode();
                    var arquivoViewModel = geraArquivo.GeraLink(httpRequest);

                    if (arquivoViewModel != null)
                    {
                        UsuarioApiLoginViewModel dadosLogin = JsonConvert.DeserializeObject<UsuarioApiLoginViewModel>(Requisicoes.AutenticaToFront(token, 2).Content);

                        var usuario = _usuarioIntermediador.GetAll().Where(u => u.IdLogin == dadosLogin.UsuarioId && u.Ativo).FirstOrDefault();

                        if (usuario != null)
                        {
                            arquivoViewModel.Log.UsuarioId = usuario.UsuarioId;

                            _logIntermediador.Add(arquivoViewModel.Log);

                            return Ok(arquivoViewModel.LinkDownload);
                        }
                        else
                        {
                            return Content(HttpStatusCode.Forbidden, "");
                        }
                    }
                    else
                    {
                        return BadRequest("Erro ao gerar o arquivo com a marca d'agua. Por favor, tente novamente.");
                    }
                }
                else
                {
                    return Content(verificaSeEstaLogado.StatusCode, "");
                }
            }
            catch
            {
                return BadRequest();
            }            
        }

        [HttpPost]
        public IHttpActionResult GeraQrCodeToFront()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var token = Request.Headers.Authorization.Parameter;

                    IRestResponse verificaSeEstaLogado = Requisicoes.AutenticaToFront(token, 2);

                    if (verificaSeEstaLogado.StatusCode == HttpStatusCode.OK)
                    {
                        IGeraArquivo geraArquivo = new QrCode();
                        var arquivoViewModel = geraArquivo.GeraLink(httpRequest);

                        if (arquivoViewModel != null)
                        {    
                            return Ok(arquivoViewModel.LinkDownload);   
                        }
                        else
                        {
                            return BadRequest("Erro ao gerar o arquivo com a marca d'agua. Por favor, tente novamente.");
                        }
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
