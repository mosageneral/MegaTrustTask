using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstraction
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
       
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        TEntity GetById(params object[] id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(params object[] id);
        void Save();

    }
}
