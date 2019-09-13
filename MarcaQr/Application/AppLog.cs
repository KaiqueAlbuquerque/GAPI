
using System;
using System.Linq;
using Application.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;
using Dominio.Interfaces.ObjVal;
using Dominio.ObjVal;

namespace Application
{
    public class AppLog : AppBase<Log>, IAppLog
    {
        private readonly IIntermediadorLog _logIntermediador;
        private readonly IIntermediadorCliente _clienteIntermediador;
        private readonly IIntermediadorUsuario _usuarioIntermediador;
        private readonly IDashboard _dashboard;


        public AppLog(IIntermediadorLog logIntermediador, IIntermediadorCliente clienteIntermediador, IIntermediadorUsuario usuarioIntermediador, IDashboard dashboard)
            : base(logIntermediador)
        {
            _logIntermediador = logIntermediador;
            _clienteIntermediador = clienteIntermediador;
            _usuarioIntermediador = usuarioIntermediador;
            _dashboard = dashboard;
        }

        public Dashboard MontaDadosDashboard(DateTime requisicoesMensal, int diaRequisicoesDiarias, int tipo)
        {
            int totalLogs = _logIntermediador.GetAll().Where(l => l.TipoAlteracao == tipo).Count();
            var logsMes = _logIntermediador.GetAll().Where(l => l.TipoAlteracao == tipo && l.DataCadastro.Month == requisicoesMensal.Month && l.DataCadastro.Year == requisicoesMensal.Year).Select(l => new RetornoLogsAux(l.DataCadastro, l.Usuario.Cliente.ClienteId)).ToList();
            var clientes = _clienteIntermediador.GetAll().Where(c => c.ApiPertencente == tipo).ToList();
            var usuarios = _usuarioIntermediador.GetAll().Where(u => u.Cliente.ApiPertencente == tipo && u.Ativo).Count();

            return _dashboard.MontaDadosDashboard(totalLogs, logsMes, clientes, usuarios, requisicoesMensal, diaRequisicoesDiarias, tipo);
        }
    }
}
