using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        DataModel Context { get; set; }
        string CombinePredicates(IList<string> predicateExpressions, string logicalFunction);
        IQueryable<T> GetAll();
        void Refresh(object entity);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        bool All(Expression<Func<T, bool>> predicate);
        //T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();

        
    }
}
