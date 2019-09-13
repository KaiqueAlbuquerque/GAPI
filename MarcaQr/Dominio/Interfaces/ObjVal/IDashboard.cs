using Dominio.Entidades;
using Dominio.ObjVal;
using System;
using System.Collections.Generic;

namespace Dominio.Interfaces.ObjVal
{
    public interface IDashboard
    {
        Dashboard MontaDadosDashboard(  int totalLogs,
                                        List<RetornoLogsAux> logs,
                                        List<Cliente> clientes,
                                        int qtdUsuarios,
                                        DateTime requisicoesMensal,
                                        int diaRequisicoesDiarias,
                                        int tipo
                                    );
    }
}
