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
    public class IntermediadorLog : IntermediadorBase<Log>, IIntermediadorLog
    {
        private readonly IRepositorioLog _logRepositorio;

        public IntermediadorLog(IRepositorioLog logRepositorio)
            :base(logRepositorio)
        {
            _logRepositorio = logRepositorio;
        }
    }
}
