using System.Collections.Generic;
using Application.Interfaces;
using Dominio.Interfaces.Intermediadores;

namespace Application
{
    public class AppBase<TEntity> : IAppBase<TEntity> where TEntity : class
    {
        private readonly IIntermediadorBase<TEntity> _intermediadorBase;

        public AppBase(IIntermediadorBase<TEntity> intermediadorBase)
        {
            _intermediadorBase = intermediadorBase;
        }

        public void Add(TEntity obj)
        {
            _intermediadorBase.Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _intermediadorBase.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _intermediadorBase.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _intermediadorBase.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _intermediadorBase.Update(obj);
        }
    }
}
