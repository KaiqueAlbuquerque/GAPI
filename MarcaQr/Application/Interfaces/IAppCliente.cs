using Dominio.Entidades;
using System.Collections;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAppCliente : IAppBase<Cliente>
    {
        void CadastraCliente(Cliente cliente);

        IEnumerable<Cliente> ConsultaClientes(int idApiPertencente, int pagina);

        IEnumerable<Cliente> ConsultaClientes(int idApiPertencente);
        
        Cliente ConsultaCliente(int idCliente);

        void AlteraCliente(Cliente cliente);

        double QuantidadePaginas(int idApiPertencente);
    }
}
