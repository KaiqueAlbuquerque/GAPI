using Servico.Interfaces;
using Servico.Interfaces.Repositorio;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico.Servico
{
    public class EmpresaServico : BaseServico<Empresa>, IEmpresaServico
    {
        private readonly IEmpresaRepositorio _empresaRepository;

        public EmpresaServico(IEmpresaRepositorio empresaRepository)
            : base(empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
    }
}
