using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Jwt.Filters;
using WebApi.Jwt.Gerenciamento;
using WebApi.Jwt.ViewModel;

namespace WebApi.Jwt.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioIntermediador _usuarioIntermediador;

        public UsuarioController(IUsuarioIntermediador usuarioIntermediador)
        {
            _usuarioIntermediador = usuarioIntermediador;
        }

        [JwtAuthentication]
        public IHttpActionResult Post([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                if(usuario != null)
                {
                    usuario.Usuario.Senha = Criptografia.Encrypt(usuario.Usuario.Senha);

                    var usuarioCadastrado = _usuarioIntermediador.CadastraUsuario(usuario.Usuario, usuario.IdsAcesso);
                    if (usuarioCadastrado != null)
                    {
                        return Ok(usuarioCadastrado);
                    }
                    else
                    {
                        return BadRequest();
                    }
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
        public IHttpActionResult Get(int idUsuario)
        {
            try
            {
                if(idUsuario == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var usuario = _usuarioIntermediador.GetById(idUsuario);

                    if (usuario != null && usuario.Ativo)
                    {
                        usuario.Senha = Criptografia.Decrypt(usuario.Senha);

                        return Ok(usuario);
                    }                        
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
        public IHttpActionResult Get(string email)
        {
            try
            {
                if(string.IsNullOrEmpty(email))
                {
                    return BadRequest();
                }
                else
                {
                    var usuario = _usuarioIntermediador.GetAll().Where(u => u.Email.Trim().ToLower() == email.Trim().ToLower() && u.Ativo).FirstOrDefault();
                    usuario.Senha = Criptografia.Decrypt(usuario.Senha);
                    return Ok(usuario);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [JwtAuthentication]
        public IHttpActionResult Get(int pagina, bool ativo)
        {
            try
            {
                UsuarioPaginacaoViewModel retorno = new UsuarioPaginacaoViewModel();
                retorno.Usuarios = new List<Usuario>();

                var usuarios = _usuarioIntermediador.GetAll().Where(u => u.Ativo).OrderBy(u => u.Nome);

                foreach(var usuario in usuarios)
                {
                    foreach(var acesso in usuario.Acessos)
                    {
                        if(acesso.AcessoId == 3)
                        {
                            usuario.Senha = Criptografia.Decrypt(usuario.Senha);
                            retorno.Usuarios.Add(usuario);
                        }
                    }
                }
                retorno.Usuarios = retorno.Usuarios.Skip(10 * pagina).Take(10).ToList();
                retorno.QuantidadePaginas = QuantidadePaginas();

                return Ok(retorno);
            }
            catch
            {
                return BadRequest();
            }
        }

        [JwtAuthentication]
        public IHttpActionResult Put([FromBody]UsuarioViewModel usuario)
        {
            try
            {
                if (usuario != null)
                {
                    usuario.Usuario.Senha = Criptografia.Encrypt(usuario.Usuario.Senha);

                    var usuarioAtualizado = _usuarioIntermediador.AtualizaUsuario(usuario.Usuario, usuario.IdsAcesso);
                    if (usuarioAtualizado != null)
                    {
                        return Ok(usuarioAtualizado);
                    }
                    else
                    {
                        return BadRequest();
                    }
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

        private double QuantidadePaginas()
        {
            int registros = _usuarioIntermediador.GetAll().Where(u => u.Ativo).Count();

            double resultado = registros / 10.0;

            var paginas = Math.Ceiling(resultado);

            return paginas;
        }
    }
}
