using Dominio.Entidades;
using Dominio.Interfaces.ObjVal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.ObjVal
{
    public class Dashboard : IDashboard
    {
        public int QuantidadeClientes { get; set; }
        public int QuantidadeUsuarios { get; set; }
        public int QuantidadeFeitas { get; set; }
        public List<RequisicoesMensal> QuantidadeRequisicoesMensal { get; set; }
        public List<RequisicoesDiarias> QuantidadeRequisicoesDiarias { get; set; }

        public Dashboard()
        {
            QuantidadeRequisicoesMensal = new List<RequisicoesMensal>();
            QuantidadeRequisicoesDiarias = new List<RequisicoesDiarias>();
        }

        public Dashboard MontaDadosDashboard(
                                                        int totalLogs,
                                                        List<RetornoLogsAux> logs,
                                                        List<Cliente> clientes,
                                                        int qtdUsuarios,
                                                        DateTime requisicoesMensal,
                                                        int diaRequisicoesDiarias,
                                                        int tipo
                                                    )
        {
            for (var i = 1; i <= requisicoesMensal.Day; i++)
            {
                RequisicoesMensal mensal = new RequisicoesMensal();
                mensal.Dia = i;
                mensal.Requisicoes = logs.Where(l => l.DataCadastro.Day == i).Count();
                QuantidadeRequisicoesMensal.Add(mensal);
            }

            QuantidadeFeitas = totalLogs;
            QuantidadeClientes = clientes.Where(c => c.Ativo).Count();
            QuantidadeUsuarios = qtdUsuarios;

            foreach (var c in clientes)
            {
                if (diaRequisicoesDiarias != 0)
                {
                    var logsFiltrados = logs.Where(l => l.DataCadastro.Day == diaRequisicoesDiarias);
                    int quantidade = logsFiltrados.Where(l => l.ClienteId == c.ClienteId).Count();
                    if (quantidade > 0)
                    {
                        RequisicoesDiarias diarias = new RequisicoesDiarias();
                        diarias.NomeCliente = c.NomeCliente;
                        diarias.Requisicoes = quantidade;
                        QuantidadeRequisicoesDiarias.Add(diarias);
                    }
                }
                else
                {
                    int quantidade = logs.Where(l => l.ClienteId == c.ClienteId).Count();
                    if (quantidade > 0)
                    {
                        RequisicoesDiarias diarias = new RequisicoesDiarias();
                        diarias.NomeCliente = c.NomeCliente;
                        diarias.Requisicoes = quantidade;
                        QuantidadeRequisicoesDiarias.Add(diarias);
                    }
                }
            }

            return this;
        }
    }

    public class RequisicoesMensal
    {
        public int Dia { get; set; }
        public int Requisicoes { get; set; }
    }

    public class RequisicoesDiarias
    {
        public string NomeCliente { get; set; }
        public int Requisicoes { get; set; }
    }

    public class RetornoLogsAux
    {
        public DateTime DataCadastro { get; set; }
        public int ClienteId { get; set; }

        public RetornoLogsAux(DateTime dataCadastro, int clienteId)
        {
            DataCadastro = dataCadastro;
            ClienteId = clienteId;
        }
    }
}
