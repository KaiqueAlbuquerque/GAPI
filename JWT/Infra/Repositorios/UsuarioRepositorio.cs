using Dominio.Entidades;
using Dominio.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Infra.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public HttpStatusCode BuscaUsuario(string username, string password, int acessoId)
        {
            Usuario usuario = GetAll().Where(u => u.Email == username && u.Senha == password && u.Ativo).FirstOrDefault();

            if(usuario != null)
            {
                if(usuario.Acessos.Count > 0)
                {
                    foreach(var acesso in usuario.Acessos.Where(a => a.Ativo))
                    {
                        if(acesso.AcessoId == acessoId)
                        {
                            return HttpStatusCode.OK;
                        }
                    }

                    return HttpStatusCode.Forbidden;
                }
                else
                {
                    return HttpStatusCode.Forbidden;
                }
            }
            else
            {
                return HttpStatusCode.Unauthorized;
            }
        }

        public Usuario CadastraUsuario(Usuario usuario, List<int> idsAcessos)
        {
            try
            {                
                if (idsAcessos.Count > 0)
                {
                    foreach (var id in idsAcessos)
                    {
                       usuario.Acessos.Add(Db.Acesso.Find(id));                        
                    }
                }

                Add(usuario);
                
                return usuario;
            }
            catch
            {
                return null;
            }            
        }

        public Usuario AtualizaUsuario(Usuario usuario, List<int> idsAcessos)
        {
            try
            {                
                Usuario usuarioBanco = GetById(usuario.UsuarioId);

                usuarioBanco.Ativo = usuario.Ativo;
                usuarioBanco.Email = usuario.Email;
                usuarioBanco.Senha = usuario.Senha;
                usuarioBanco.Nome = usuario.Nome;

                usuarioBanco.Acessos.Clear();                

                if (idsAcessos.Count > 0)
                {
                    foreach (var id in idsAcessos)
                    {
                        usuarioBanco.Acessos.Add(Db.Acesso.Find(id));
                    }
                }

                Update(usuarioBanco);

                return usuarioBanco;
            }
            catch
            {
                return null;
            }
        }
    }
}
