using Servico.Interfaces.Repositorio;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico.Servico
{
    public class LogsServico : BaseServico<Logs>, ILogsServico
    {
        private readonly ILogsRepositorio _logsRepository;

        public LogsServico(ILogsRepositorio logsRepository)
            : base(logsRepository)
        {
            _logsRepository = logsRepository;
        }
    }
}
