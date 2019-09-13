using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Jwt.ViewModel
{
    public class UsuarioPaginacaoViewModel
    {
        public IList<Usuario> Usuarios { get; set; }

        public double QuantidadePaginas { get; set; }
    }
}