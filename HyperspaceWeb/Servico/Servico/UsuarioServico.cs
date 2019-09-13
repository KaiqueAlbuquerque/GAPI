using Servico.Interfaces.Repositorio;
using Servico.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico.Servico
{
    public class UsuarioServico : BaseServico<Usuarios>, IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepository;

        public UsuarioServico(IUsuarioRepositorio usuarioRepository)
            : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
    }
}
