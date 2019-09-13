using API.ViewModel;
using AutoMapper;
using Servico;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly ITokenServico _tokenServico;

        public UsuarioController(IUsuarioServico usuarioServico, ITokenServico tokenServico)
        {
            _usuarioServico = usuarioServico;
            _tokenServico = tokenServico;
        }

        public IHttpActionResult Post([FromBody]UsuarioViewModel UsuarioViewModel)
        {
            try
            {
                var usuario = Mapper.Map<UsuarioViewModel, Usuarios>(UsuarioViewModel);

                var usuarioCadastrado = _usuarioServico.GetAll().Where(u => u.Token.Empresas.Id_Empresa == int.Parse(UsuarioViewModel.Id_Empresa) && u.IdUsuarioDv == usuario.IdUsuarioDv).FirstOrDefault();

                if (usuario.Id_Token == 0)
                {
                    if (usuarioCadastrado != null)
                    {
                        usuarioCadastrado.Id_Token = _tokenServico.GetAll().Where(t => t.Id_Empresa == int.Parse(UsuarioViewModel.Id_Empresa) && t.Token_Key == "Token Usuario Desativado").FirstOrDefault().Id_Token;
                        _usuarioServico.Update(usuarioCadastrado);
                    }

                    return Ok();
                }
                else
                {
                    if (usuarioCadastrado != null)
                    {
                        usuarioCadastrado.Id_Token = usuario.Id_Token;                        
                        _usuarioServico.Update(usuarioCadastrado);
                        return Ok();
                    }
                    else
                    {
                        _usuarioServico.Add(usuario);
                        return Ok();
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
