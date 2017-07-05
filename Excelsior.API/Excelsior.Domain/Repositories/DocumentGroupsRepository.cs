using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class DocumentGroupsRepository : EntityBaseRepository<DOCU_DocumentGroup>, IDocumentGroupsRepository
    {
        #region Constructor

        public DocumentGroupsRepository(DataModel context) : base(context)
        {
        }

        #endregion
    }
}
