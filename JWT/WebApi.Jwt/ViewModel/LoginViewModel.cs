using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Jwt.ViewModel
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public int AcessoId { get; set; }
    }
}