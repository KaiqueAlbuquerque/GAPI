using API.Business;
using API.ViewModel;
using Servico;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogsController : ApiController
    {
        private readonly ILogsServico _logsServico;
        private readonly ITokenServico _tokensServico;
        private readonly IEmpresaServico _empresaServico;

        public LogsController(
                                ILogsServico logsServico,
                                ITokenServico tokensServico,
                                IEmpresaServico empresaServico
                             )
        {
            _logsServico = logsServico;
            _tokensServico = tokensServico;
            _empresaServico = empresaServico;
        }

        public IHttpActionResult Get(DateTime requisicoesMensal, int diaRequisicoesDiarias)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var tokenJwt = Request.Headers.Authorization.Parameter;

                HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                if (verificaSeEstaLogado == HttpStatusCode.OK)
                {
                    int totalLogs = _logsServico.GetAll().Count();
                    var logsMes = _logsServico.GetAll().Where(l => l.criacao.Month == requisicoesMensal.Month && l.criacao.Year == requisicoesMensal.Year).Select(l => new RetornoLogsAux(l.criacao, l.Id_Empresa)).ToList();
                    var tokens = _tokensServico.GetAll().Where(t => t.Data_Expiracao >= DateTime.Now).Count();
                    var clientes = _empresaServico.GetAll().ToList();
                    
                    return Ok(DashBoardBusiness.MontaDadosDashboard(totalLogs, logsMes, clientes, tokens, requisicoesMensal, diaRequisicoesDiarias));
                }
                else
                {
                    throw new HttpResponseException(verificaSeEstaLogado);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
