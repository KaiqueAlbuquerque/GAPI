using Dominio.Entidades;
using System.Collections.Generic;

namespace ApiQrCode.ViewModel
{
    public class UsuariosViewModel
    {
        public IEnumerable<Usuario> Usuarios { get; set; }

        public double QuantidadePaginas { get; set; }
    }
}