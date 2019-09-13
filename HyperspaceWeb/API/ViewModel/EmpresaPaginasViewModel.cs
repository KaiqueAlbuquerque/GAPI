using Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModel
{
    public class EmpresaPaginasViewModel
    {
        public IEnumerable<Empresa> Empresas { get; set; }

        public double QuantidadePaginas { get; set; }
    }
}