using API.ViewModel;
using Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Business
{
    public static class DashBoardBusiness
    {
        public static DashboardViewModel MontaDadosDashboard(
                                                        int totalLogs,
                                                        List<RetornoLogsAux> logs,
                                                        List<Empresa> clientes,
                                                        int qtdTokens,
                                                        DateTime requisicoesMensal,
                                                        int diaRequisicoesDiarias
                                                    )
        {
            DashboardViewModel dash = new DashboardViewModel();

            for (var i = 1; i <= requisicoesMensal.Day; i++)
            {
                RequisicoesMensal mensal = new RequisicoesMensal();
                mensal.Dia = i;
                mensal.Requisicoes = logs.Where(l => l.Cadastro.Day == i).Count();
                dash.QuantidadeRequisicoesMensal.Add(mensal);
            }

            dash.QuantidadeFeitas = totalLogs;
            dash.QuantidadeClientes = clientes.Where(e => e.Ativo).Count();
            dash.QuantidadeTokens = qtdTokens;

            foreach (var c in clientes)
            {
                if (diaRequisicoesDiarias != 0)
                {
                    var logsFiltrados = logs.Where(l => l.Cadastro.Day == diaRequisicoesDiarias);
                    int quantidade = logsFiltrados.Where(l => l.ClienteId == c.Id_Empresa).Count();
                    if(quantidade > 0)
                    {
                        RequisicoesDiarias diarias = new RequisicoesDiarias();
                        diarias.NomeCliente = c.Nome;
                        diarias.Requisicoes = quantidade;
                        dash.QuantidadeRequisicoesDiarias.Add(diarias);
                    }                    
                }
                else
                {
                    int quantidade = logs.Where(l => l.ClienteId == c.Id_Empresa).Count();
                    if(quantidade > 0)
                    {
                        RequisicoesDiarias diarias = new RequisicoesDiarias();
                        diarias.NomeCliente = c.Nome;
                        diarias.Requisicoes = quantidade;
                        dash.QuantidadeRequisicoesDiarias.Add(diarias);
                    }                    
                }
            }

            return dash;
        }
    }
}