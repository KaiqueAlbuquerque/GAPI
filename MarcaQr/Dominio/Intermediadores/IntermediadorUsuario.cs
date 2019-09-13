using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;
using Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Intermediadores
{
    public class IntermediadorUsuario : IntermediadorBase<Usuario>, IIntermediadorUsuario
    {
        private readonly IRepositorioUsuario _usuarioRepositorio;

        public IntermediadorUsuario(IRepositorioUsuario usuarioRepositorio)
            : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
    }
}
