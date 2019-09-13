using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAppBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Update(TEntity obj);

        void Remove(TEntity obj);
    }
}
