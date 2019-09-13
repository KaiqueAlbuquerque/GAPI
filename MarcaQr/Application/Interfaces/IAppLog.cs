using Dominio.Entidades;
using Dominio.ObjVal;
using System;

namespace Application.Interfaces
{
    public interface IAppLog : IAppBase<Log>
    {
        Dashboard MontaDadosDashboard(DateTime requisicoesMensal, int diaRequisicoesDiarias, int tipo);
    }
}
