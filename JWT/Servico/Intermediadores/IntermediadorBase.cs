using Dominio.Interfaces;
using Dominio.Interfaces.Intermediadores;
using System;
using System.Collections.Generic;

namespace Dominio.Intermediadores
{
    public class IntermediadorBase<TEntity> : IDisposable, IIntermediadorBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repositorio;

        public IntermediadorBase(IRepositorioBase<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public void Add(TEntity obj)
        {
            _repositorio.Add(obj);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repositorio.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repositorio.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _repositorio.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _repositorio.Update(obj);
        }
    }
}
