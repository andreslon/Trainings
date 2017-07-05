using Excelsior.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Excelsior.Domain.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : class, new()
    {
        public DataModel Context { get; set; }

        #region Constructor

        public EntityBaseRepository(DataModel context)
        {
            Context = context;
        }

        #endregion

        #region Functions

        protected virtual IQueryable ApplySortExpression(string sort, IQueryable query)
        {
            var sortItems = sort.ToLower().Split(',');
            string orderby = string.Empty;
            foreach (var item in sortItems)
            {
                var field = ApplySortField(item);
                if (!string.IsNullOrEmpty(field))
                {
                    if (string.IsNullOrEmpty(orderby))
                        orderby = ApplySortField(item);
                    else
                        orderby = string.Format("{0}, {1}", orderby, ApplySortField(item));
                }
            }

            return query.OrderBy(orderby);
        }

        protected virtual string ApplySortField(string item)
        {
            return item;
        }

        public string CombinePredicates(IList<string> predicateExpressions, string logicalFunction)
        {
            for(var i = 0; i < predicateExpressions.Count; i++)
            {
                predicateExpressions[i] = string.Format("({0})", predicateExpressions[i]);
            }
            var result = string.Join(string.Format(" {0} ", logicalFunction), predicateExpressions);
            return result;
        }

        public void Refresh(object entity)
        {
            Context.Refresh(Telerik.OpenAccess.RefreshMode.OverwriteChangesFromStore, entity);
        }

        public virtual int Count()
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return GetAll().Count();
            });
            return result;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return GetAll().Count(predicate);
            });
            return result;
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return GetAll().Any(predicate);
            });
            return result;
        }

        public virtual bool All(Expression<Func<T, bool>> predicate)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return GetAll().All(predicate);
            });
            return result;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Context.GetAll<T>();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return GetAll().FirstOrDefault(predicate);
            });
            return result;
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            Context.Add(entity);
        }

        public virtual void Update(T entity)
        {
            var dbEntityEntry = Context.AttachCopy(entity);
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = Context.AttachCopy(entity);
            Context.Delete(entity);
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = FindBy(predicate);
            Context.Delete(entities);
        }

        public virtual void Commit()
        {            
            DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                Context.SaveChanges();
            });            
        }

        public virtual void Flush()
        {
            DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                Context.FlushChanges();
            });
        }

        #endregion
    }
}