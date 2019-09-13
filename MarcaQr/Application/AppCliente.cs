using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Intermediadores;

namespace Application
{
    public class AppCliente : AppBase<Cliente>, IAppCliente
    {
        private readonly IIntermediadorCliente _intermediadorCliente;
        private readonly IIntermediadorUsuario _intermediadorUsuario;
        
        public AppCliente(IIntermediadorCliente intermediadorCliente, IIntermediadorUsuario intermediadorUsuario)
            :base(intermediadorCliente)
        {
            _intermediadorCliente = intermediadorCliente;
            _intermediadorUsuario = intermediadorUsuario;
        }

        public void AlteraCliente(Cliente cliente)
        {
            var clienteCadastrado = _intermediadorCliente.GetById(cliente.ClienteId);

            if (clienteCadastrado != null)
            {
                clienteCadastrado.Ativo = cliente.Ativo;
                clienteCadastrado.NomeCliente = cliente.NomeCliente;

                _intermediadorCliente.Update(clienteCadastrado);

                if(cliente.Ativo == false)
                {
                    var usuarios = _intermediadorUsuario.GetAll().Where(u => u.ClienteId == cliente.ClienteId && u.Ativo);

                    foreach(var usuario in usuarios)
                    {
                        usuario.Ativo = false;
                        _intermediadorUsuario.Update(usuario);
                    }
                }
            }
        }

        public void CadastraCliente(Cliente cliente)
        {
            cliente.Ativo = true;
            _intermediadorCliente.Add(cliente);
        }

        public Cliente ConsultaCliente(int idCliente)
        {
            return _intermediadorCliente.GetById(idCliente);
        }

        public IEnumerable<Cliente> ConsultaClientes(int idApiPertencente)
        {
            return _intermediadorCliente.GetAll().Where(c => c.Ativo && c.ApiPertencente == idApiPertencente).OrderBy(c => c.NomeCliente);
        }

        public IEnumerable<Cliente> ConsultaClientes(int idApiPertencente, int pagina)
        {
            return _intermediadorCliente.GetAll().Where(c => c.Ativo && c.ApiPertencente == idApiPertencente).OrderBy(c => c.NomeCliente).Skip(10 * pagina).Take(10);
        }

        public double QuantidadePaginas(int idApiPertencente)
        {
            int registros = _intermediadorCliente.GetAll().Where(c => c.Ativo && c.ApiPertencente == idApiPertencente).Count();

            double resultado = registros / 10.0;

            var paginas = Math.Ceiling(resultado);

            return paginas;
        }
    }
}
