using API.Business;
using API.ViewModel;
using AutoMapper;
using RestSharp;
using Servico;
using Servico.Interfaces;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TokenController : ApiController
    {
        private readonly ITokenServico _tokenServico;
        private readonly IEmpresaServico _empresaServico;
        private readonly IUsuarioServico _usuarioServico;

        public TokenController(ITokenServico tokenServico, IEmpresaServico empresaServico, IUsuarioServico usuarioServico)
        {
            _tokenServico = tokenServico;
            _empresaServico = empresaServico;
            _usuarioServico = usuarioServico;
        }

        public IHttpActionResult Post([FromBody]TokenViewModel TokenViewModel)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var tokenJwt = Request.Headers.Authorization.Parameter;

                HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                if (verificaSeEstaLogado == HttpStatusCode.OK)
                {
                    int quantidade = int.Parse(TokenViewModel.Quantidade);

                    if (quantidade > 0)
                    {
                        var token = Mapper.Map<TokenViewModel, Token>(TokenViewModel);

                        var client = new RestClient(ConfigurationManager.AppSettings["linkGeraToken"] + "criptografar=false&tipo=MD5&tamanho=25&quantidade=" + quantidade);
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);

                        string[] tokens = response.Content.Split(',');

                        tokens[0] = tokens[0].Replace("[", "");
                        tokens[tokens.Length - 1] = tokens[tokens.Length - 1].Replace("]", "");

                        for (var i = 0; i < tokens.Length; i++)
                        {
                            var tok = new Token();
                            tok.Id_Empresa = token.Id_Empresa;
                            tok.Data_Expiracao = token.Data_Expiracao;
                            tok.Token_Key = tokens[i].Replace("\"", "");

                            _tokenServico.Add(tok);
                        }

                        return Ok();
                    }
                    return BadRequest();
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

        public IHttpActionResult Get(int idEmpresa, int idUsuarioDv)
        {
            try
            {
                List<TokenViewModel> tokenViewModel = new List<TokenViewModel>();

                var tokens = from t in _tokenServico.GetAll()
                             where t.Id_Empresa == idEmpresa && !(_usuarioServico.GetAll().Any(u => u.Id_Token == t.Id_Token)) &&
                             t.Token_Key != "Token Usuario Desativado"
                             select t;

                foreach(var t in tokens)
                {
                    var token = Mapper.Map<Token, TokenViewModel>(t);
                    token.Checked = false;
                    tokenViewModel.Add(token);
                }
                
                if(idUsuarioDv != 0)
                {
                    var tokenUsuario = from t in _tokenServico.GetAll()
                                       where t.Id_Empresa == idEmpresa && (_usuarioServico.GetAll().Any(u => u.Id_Token == t.Id_Token && u.IdUsuarioDv == idUsuarioDv))
                                       select t;

                    if(tokenUsuario.Count() > 0 && tokenUsuario != null && tokenUsuario.FirstOrDefault() != null)
                    {
                        var token = Mapper.Map<Token, TokenViewModel>(tokenUsuario.FirstOrDefault());
                        token.Checked = true;
                        tokenViewModel.Add(token);
                    }
                }

                return Ok(tokenViewModel);
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
                var tokenJwt = Request.Headers.Authorization.Parameter;

                HttpStatusCode verificaSeEstaLogado = Autenticacao.Autentica(tokenJwt, 3);

                if (verificaSeEstaLogado == HttpStatusCode.OK)
                {
                    var tokens = _tokenServico.GetAll().Where(t => t.Id_Empresa == idEmpresa && t.Data_Expiracao >= DateTime.Now);

                    return Ok(tokens);
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
                        EmpresaPaginasViewModel tokens = new EmpresaPaginasViewModel();

                        tokens.Empresas = _empresaServico.GetAll().Where(e => e.Ativo).OrderBy(e => e.Nome).Skip(10 * pagina).Take(10);

                        foreach (var empresa in tokens.Empresas)
                        {
                            empresa.Tokens = _tokenServico.GetAll().Where(t => t.Id_Empresa == empresa.Id_Empresa && t.Data_Expiracao >= DateTime.Now).ToList();
                        }

                        int registros = _empresaServico.GetAll().Where(c => c.Ativo).Count();
                        double resultado = registros / 10.0;
                        var paginas = Math.Ceiling(resultado);

                        tokens.QuantidadePaginas = paginas;

                        return Ok(tokens);
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