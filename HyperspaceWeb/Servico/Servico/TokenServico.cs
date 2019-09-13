using Servico.Interfaces.Repositorio;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico.Servico
{
    public class TokenServico : BaseServico<Token>, ITokenServico
    {
        private readonly ITokenRepositorio _tokenRepository;

        public TokenServico(ITokenRepositorio tokenRepository)
            : base(tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
    }
}
