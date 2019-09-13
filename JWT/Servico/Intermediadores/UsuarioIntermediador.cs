using System.Collections.Generic;
using System.Net;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Intermediadores;

namespace Dominio.Intermediadores
{
    public class UsuarioIntermediador : IntermediadorBase<Usuario>, IUsuarioIntermediador
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioIntermediador(IUsuarioRepositorio usuarioRepositorio)
            : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public HttpStatusCode BuscaUsuario(string username, string password, int acessoId)
        {
            return _usuarioRepositorio.BuscaUsuario(username, password, acessoId);
        }

        public Usuario CadastraUsuario(Usuario usuario, List<int> idsAcessos)
        {
            return _usuarioRepositorio.CadastraUsuario(usuario, idsAcessos);
        }

        public Usuario AtualizaUsuario(Usuario usuario, List<int> idsAcessos)
        {
            return _usuarioRepositorio.AtualizaUsuario(usuario, idsAcessos);
        }
    }
}
