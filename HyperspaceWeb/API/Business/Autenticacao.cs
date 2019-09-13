using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace API.Business
{
    public static class Autenticacao
    {
        public static HttpStatusCode Autentica(string token, int tipo)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["linkAutenticacao"] + tipo);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            return response.StatusCode;
        }
    }
}