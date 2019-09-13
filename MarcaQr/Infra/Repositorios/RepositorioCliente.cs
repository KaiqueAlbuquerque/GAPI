using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class RepositorioCliente : RepositorioBase<Cliente>, IRepositorioCliente
    {
    }
}
