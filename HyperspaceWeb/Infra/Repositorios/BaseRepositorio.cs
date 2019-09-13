using Servico.Interfaces;
using Servico.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infra.Repositorios
{
    public class BaseRepositorio<TEntity> : IDisposable, IBaseRepositorio<TEntity> where TEntity : class
    {
        protected Context Db = new Context();

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();         
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            Db.Set<TEntity>().Attach(obj);
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
