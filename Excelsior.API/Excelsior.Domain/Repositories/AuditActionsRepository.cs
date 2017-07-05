using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class AuditActionsRepository : EntityBaseRepository<AUDIT_Action>, IAuditActionsRepository
    {
        #region Constructor

        public AuditActionsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        #endregion
    }
}