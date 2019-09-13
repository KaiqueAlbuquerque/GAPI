using Dominio.Entidades;
using System.Collections.Generic;
using System.Net;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        HttpStatusCode BuscaUsuario(string username, string password, int acessoId);

        Usuario CadastraUsuario(Usuario usuario, List<int> idsAcessos);

        Usuario AtualizaUsuario(Usuario usuario, List<int> idsAcessos);
    }
}
