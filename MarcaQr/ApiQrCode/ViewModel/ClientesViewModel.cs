using Dominio.Entidades;
using System.Collections.Generic;

namespace ApiQrCode.ViewModel
{
    public class ClientesViewModel
    {
        public IEnumerable<Cliente> Clientes { get; set; }

        public double QuantidadePaginas { get; set; }
    }
}