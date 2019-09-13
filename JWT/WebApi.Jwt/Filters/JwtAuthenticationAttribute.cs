using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Intermediadores;
using Dominio.Intermediadores;
using Infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WebApi.Jwt.Filters
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

            else
                context.Principal = principal;
        }
               
        private static bool ValidateToken(string token, out string email)
        {
            email = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var emailClaim = identity.FindFirst(ClaimTypes.Email);
            email = emailClaim?.Value;
                        
            if (string.IsNullOrEmpty(email))
                return false;

            // More validate to check whether username exists in system
            return(VerificaSeUsuarioExiste(email));
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            string email;

            if (ValidateToken(token, out email))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email)
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }

        private static bool VerificaSeUsuarioExiste(string email)
        {
            IUsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            Usuario usuario = usuarioRepositorio.GetAll().Where(u => u.Email == email && u.Ativo).FirstOrDefault();

            if (usuario != null)
                return true;
            else
                return false;
        }

        public string RetornaEmailToken(string token)
        {
            string email = "";

            var simplePrinciple = JwtManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return "";

            if (!identity.IsAuthenticated)
                return "";

            var emailClaim = identity.FindFirst(ClaimTypes.Email);
            email = emailClaim?.Value;

            if (string.IsNullOrEmpty(email))
                return "";
            else
                return email;            
        }
    }
}
