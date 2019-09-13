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
    public class IntermediadorCliente : IntermediadorBase<Cliente>, IIntermediadorCliente
    {
        private readonly IRepositorioCliente _clienteRepositorio;

        public IntermediadorCliente(IRepositorioCliente clienteRepositorio)
            : base(clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }
    }
}
