using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Features.Core
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        void Add(TEntity obj);
        TEntity GetToId(Guid id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Delete(Guid id);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        int Commit();
    }
}
