using API.Business;
using API.ViewModel;
using AutoMapper;
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
    public class EmpresaController : ApiController
    {
        private readonly IEmpresaServico _empresaServico;

        public EmpresaController(IEmpresaServico empresaServico)
        {
            _empresaServico = empresaServico;
        }

        public IHttpActionResult Post([FromBody]EmpresaViewModel EmpresaViewModel)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var tokenJwt = Request.Headers.Authorization.Parameter;

                    HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                    if (verificaSeEstaLogado == HttpStatusCode.OK)
                    {
                        var empresa = Mapper.Map<EmpresaViewModel, Empresa>(EmpresaViewModel);

                        Random random = new Random();
                        empresa.Codigo_Empresa = random.Next(100000, 999999);

                        _empresaServico.Add(empresa);

                        return Ok(empresa);
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado, "");
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

        public IHttpActionResult Get()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var tokenJwt = Request.Headers.Authorization.Parameter;

                    HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                    if (verificaSeEstaLogado == HttpStatusCode.OK)
                    {
                        return Ok(_empresaServico.GetAll().Where(e => e.Ativo).OrderBy(e => e.Nome));
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado, "");
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

        public IHttpActionResult Get(int pagina, bool ativo)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                    
                if(Request.Headers.Authorization != null)
                {
                    var tokenJwt = Request.Headers.Authorization.Parameter;

                    HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                    if (verificaSeEstaLogado == HttpStatusCode.OK)
                    {
                        EmpresaPaginasViewModel empresas = new EmpresaPaginasViewModel();
                        empresas.Empresas = _empresaServico.GetAll().Where(e => e.Ativo).OrderBy(e => e.Nome).Skip(10 * pagina).Take(10);

                        int registros = _empresaServico.GetAll().Where(c => c.Ativo).Count();
                        double resultado = registros / 10.0;
                        var paginas = Math.Ceiling(resultado);

                        empresas.QuantidadePaginas = paginas;
                        return Ok(empresas);
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado, "");
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

        public IHttpActionResult Get(int idEmpresa)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                
                if (Request.Headers.Authorization != null)
                {
                    var tokenJwt = Request.Headers.Authorization.Parameter;

                    HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                    if (verificaSeEstaLogado == HttpStatusCode.OK)
                    {
                        var empresa = _empresaServico.GetById(idEmpresa);

                        if (empresa.Ativo)
                            return Ok(empresa);
                        else
                            return BadRequest("Empresa não está ativa.");
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado, "");
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

        public IHttpActionResult Put([FromBody]EmpresaViewModel EmpresaViewModel)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (Request.Headers.Authorization != null)
                {
                    var tokenJwt = Request.Headers.Authorization.Parameter;

                    HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                    if (verificaSeEstaLogado == HttpStatusCode.OK)
                    {
                        var empresa = Mapper.Map<EmpresaViewModel, Empresa>(EmpresaViewModel);

                        var empresaCadastrada = _empresaServico.GetById(empresa.Id_Empresa);

                        empresaCadastrada.Ativo = empresa.Ativo;
                        empresaCadastrada.Nome = empresa.Nome;

                        _empresaServico.Update(empresaCadastrada);

                        return Ok(empresaCadastrada);
                    }
                    else
                    {
                        return Content(verificaSeEstaLogado, "");
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
