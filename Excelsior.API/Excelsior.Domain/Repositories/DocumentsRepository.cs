using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class DocucmentsRepository : EntityBaseRepository<DOCU_Document>, IDocumentsRepository
    {
        #region Constructor

        public DocucmentsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<DOCU_Document> GetAll(long? studyId, string userId, bool? isActive, string search)
        {
            var query = GetAll();
            //find the user
            CONTACT_User u = null;
            if (!string.IsNullOrEmpty(userId))
            {
                u = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == userId);
                });
            }
            if (u != null)
            {
                var roles = new List<string> { };
                switch (u.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    //case "project manager":
                        break;
                    default:
                        query = query.Where(x => x.DOCU_DocumentRoles.Any(y => y.RoleId == u.RoleId));
                        break;
                }
            }
            
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            }
            if (studyId.HasValue)
            {
                query = query.Where(x => x.DOCUDocumentGroup.TrialID == studyId);
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.DocumentName.Contains(search));
            }
            return query;
        }

        public override void Delete(DOCU_Document entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<DOCU_Document, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }
        #endregion
    }
}
