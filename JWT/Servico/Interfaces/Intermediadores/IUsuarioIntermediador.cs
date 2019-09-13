using Dominio.Entidades;
using System.Collections.Generic;
using System.Net;

namespace Dominio.Interfaces.Intermediadores
{
    public interface IUsuarioIntermediador : IIntermediadorBase<Usuario>
    {
        HttpStatusCode BuscaUsuario(string username, string password, int acessoId);

        Usuario CadastraUsuario(Usuario usuario, List<int> idsAcessos);

        Usuario AtualizaUsuario(Usuario usuario, List<int> idsAcessos);
    }
}
