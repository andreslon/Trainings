using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class DocumentVersionsRepository : EntityBaseRepository<DOCU_DocumentVersion>, IDocumentVersionsRepository
    {
        #region Constructor

        public DocumentVersionsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<DOCU_DocumentVersion> GetAll(long? studyId, long? documentId, bool? isActive)
        {
            var query = GetAll();

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            }
            if (studyId.HasValue)
            {
                query = query.Where(x => x.DOCUDocument.DOCUDocumentGroup.TrialID == studyId);
            }
            if (documentId.HasValue)
            {
                query = query.Where(x => x.DocumentID == documentId);
            }
            return query;
        }
        #endregion
    }
}