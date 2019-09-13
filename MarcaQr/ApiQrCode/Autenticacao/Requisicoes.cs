using RestSharp;
using System.Configuration;

namespace ApiAlteraPDF.Autenticacao
{
    public static class Requisicoes
    {
        public static IRestResponse AutenticaToBack(string email, string senha, int tipo)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["linkAutenticacao"]);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"Email\":\"" + email + "\",\n\t\"Senha\":\"" + senha + "\",\n\t\"AcessoId\":\"" + tipo + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }

        public static IRestResponse AutenticaToFront(string token, int tipo)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["linkVerificaSeLogado"] + tipo);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
