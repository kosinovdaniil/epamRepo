using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> f);
        TEntity Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity entity);
    }
}