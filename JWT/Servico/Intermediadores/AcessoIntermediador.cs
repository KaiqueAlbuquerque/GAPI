using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Intermediadores;

namespace Dominio.Intermediadores
{
    public class AcessoIntermediador : IntermediadorBase<Acesso>, IAcessoIntermediador
    {
        private readonly IAcessoRepositorio _acessoRepositorio;

        public AcessoIntermediador(IAcessoRepositorio acessoRepositorio)
            : base(acessoRepositorio)
        {
            _acessoRepositorio = acessoRepositorio;
        }
    }
}
