using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;

namespace Application
{
    public class AppUsuario : AppBase<Usuario>, IAppUsuario
    {
        private readonly IIntermediadorUsuario _intermediadorUsuario;

        public AppUsuario(IIntermediadorUsuario intermediadorUsuario)
            : base(intermediadorUsuario)
        {
            _intermediadorUsuario = intermediadorUsuario;
        }

        public void AlteraUsuario(Usuario usuario)
        {
            var usuarioCadastrado = _intermediadorUsuario.GetById(usuario.UsuarioId);

            if (usuarioCadastrado != null)
            {
                usuarioCadastrado.Ativo = usuario.Ativo;
                usuarioCadastrado.ClienteId = usuario.ClienteId;

                _intermediadorUsuario.Update(usuarioCadastrado);
            }
        }

        public void CadastraUsuario(Usuario usuario)
        {
            usuario.Ativo = true;
            _intermediadorUsuario.Add(usuario);
        }

        public Usuario ConsultaUsuario(int idUsuario)
        {
            return _intermediadorUsuario.GetById(idUsuario);
        }

        public bool ConsultaUsuarioIdLogin(int idLogin, int idApiPertencente)
        {
            var usuario = _intermediadorUsuario.GetAll().Where(u => u.Ativo && u.IdLogin == idLogin && u.Cliente.ApiPertencente == idApiPertencente).FirstOrDefault();

            if(usuario != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Usuario> ConsultaUsuarios(int? idCliente, int idApiPertencente, int pagina)
        {
            IEnumerable<Usuario> usuarios;

            if (idCliente == 0 || idCliente == null)
            {
                usuarios = _intermediadorUsuario.GetAll().Where(u => u.Ativo && u.Cliente.ApiPertencente == idApiPertencente).Skip(10 * pagina).Take(10); 
            }
            else
            {
                usuarios = _intermediadorUsuario.GetAll().Where(u => u.ClienteId == idCliente && u.Ativo).Skip(10 * pagina).Take(10);
            }

            return usuarios;
        }

        public double QuantidadePaginas()
        {
            int registros = _intermediadorUsuario.GetAll().Where(u => u.Ativo && u.Cliente.ApiPertencente == 1).Count();

            double resultado = registros / 10.0;

            var paginas = Math.Ceiling(resultado);

            return paginas;
        }
    }
}
