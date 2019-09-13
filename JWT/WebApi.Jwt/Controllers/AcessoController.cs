using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Jwt.Filters;

namespace WebApi.Jwt.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AcessoController : ApiController
    {
        private readonly IAcessoIntermediador _acessoIntermediador;

        public AcessoController(IAcessoIntermediador acessoIntermediador)
        {
            _acessoIntermediador = acessoIntermediador;
        }

        [JwtAuthentication]
        public IHttpActionResult Post([FromBody] Acesso acesso)
        {
            try
            {
                if (acesso != null)
                {
                    _acessoIntermediador.Add(acesso);
                    return Ok();
                }
                else
                {
                    return BadRequest("Dados inválidos");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [JwtAuthentication]
        public IHttpActionResult Get(int idAcesso)
        {
            try
            {
                if (idAcesso == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var acesso = _acessoIntermediador.GetById(idAcesso);

                    if (acesso != null && acesso.Ativo)
                        return Ok(acesso);
                    else
                        return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [JwtAuthentication]
        public IHttpActionResult Get()
        {
            try
            {
                var acessos = _acessoIntermediador.GetAll().Where(a => a.Ativo);

                return Ok(acessos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [JwtAuthentication]
        public IHttpActionResult Put([FromBody]Acesso acesso)
        {
            try
            {
                if (acesso != null)
                {
                    _acessoIntermediador.Update(acesso);

                    return Ok();
                }
                else
                {
                    return BadRequest("Dados inválidos");
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
