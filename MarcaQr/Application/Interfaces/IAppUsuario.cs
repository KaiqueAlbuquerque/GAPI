using Dominio.Entidades;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAppUsuario : IAppBase<Usuario>
    {
        void CadastraUsuario(Usuario usuario);

        IEnumerable<Usuario> ConsultaUsuarios(int? idCliente, int idApiPertencente, int pagina);

        Usuario ConsultaUsuario(int idUsuario);

        bool ConsultaUsuarioIdLogin(int idLogin, int idApiPertencente);

        void AlteraUsuario(Usuario usuario);

        double QuantidadePaginas();
    }
}
