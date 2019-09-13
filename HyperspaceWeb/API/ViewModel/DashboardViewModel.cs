using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModel
{
    public class DashboardViewModel
    {
        public int QuantidadeClientes { get; set; }
        public int QuantidadeTokens { get; set; }
        public int QuantidadeFeitas { get; set; }
        public List<RequisicoesMensal> QuantidadeRequisicoesMensal { get; set; }
        public List<RequisicoesDiarias> QuantidadeRequisicoesDiarias { get; set; }

        public DashboardViewModel()
        {
            QuantidadeRequisicoesMensal = new List<RequisicoesMensal>();
            QuantidadeRequisicoesDiarias = new List<RequisicoesDiarias>();
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
        public DateTime Cadastro { get; set; }

        public int ClienteId { get; set; }

        public RetornoLogsAux(DateTime cadastro, int clienteId)
        {
            Cadastro = cadastro;
            ClienteId = clienteId;
        }
    }
}