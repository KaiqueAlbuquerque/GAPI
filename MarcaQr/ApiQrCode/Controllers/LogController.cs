using ApiAlteraPDF.Autenticacao;
using Application.Interfaces;
using RestSharp;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiAlteraPDF.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogController : ApiController
    {
        private readonly IAppLog _appLog;
        
        public LogController(IAppLog appLog)
        {
            _appLog = appLog;
        }

        [HttpGet]
        public IHttpActionResult Dashboard(DateTime requisicoesMensal, int diaRequisicoesDiarias, int tipo)
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
                        return Ok(_appLog.MontaDadosDashboard(requisicoesMensal, diaRequisicoesDiarias, tipo));
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
