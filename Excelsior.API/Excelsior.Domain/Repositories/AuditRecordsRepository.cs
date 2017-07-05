using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using Excelsior.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class AuditRecordsRepository : EntityBaseRepository<AUDIT_Record>, IAuditRecordsRepository
    {
        #region Constructor

        public IResourceOwnerData ResourceOwnerData { get; set; }
        public IUsersRepository UsersRepository { get; set; }
        public IAuthClientRepository AuthClientRepository { get; set; }
        public IAuditActionsRepository AuditActionsRepository { get; set; }
        public AuditRecordsRepository(DataModel context, IResourceOwnerData resourceOwnerData
            , IUsersRepository usersRepository
            , IAuthClientRepository authClientRepository
            , IAuditActionsRepository auditActionsRepository) : base(context)
        {
            ResourceOwnerData = resourceOwnerData;
            UsersRepository = usersRepository;
            AuthClientRepository = authClientRepository;
            AuditActionsRepository = auditActionsRepository;
        }

        #endregion

        #region Functions

        public IQueryable<AUDIT_Record> GetAll(DateTime? startDate, DateTime? endDate, string search)
        {
            var records = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                records = records.Where(x => x.CONTACTUser.LastName.Contains(search)
                    || x.CONTACTUser.FirstName.Contains(search)
                    || x.CONTACTUser.LoweredUserName.Contains(search)
                    || x.CONTACTUser.Email.Contains(search)
                    || x.CONTACTUser.CONTACTAffiliation.AffiliationName.Contains(search)
                    || (x.CONTACTUser.CONTACTAffiliation.CONTACTCountry == null ? false : x.CONTACTUser.CONTACTAffiliation.CONTACTCountry.CountryName.Contains(search))
                    //Trial
                    //Subject
                    //Serie
                    //CERTUser
                    //CERTEquipment
                    //CERTUploadInfo
                    );
            }

            if (startDate.HasValue)
            {
                records = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return records.Where(x => x.PerformedDateTime >= startDate);
                });
            }
            if (endDate.HasValue)
            {
                records = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return records.Where(x => x.PerformedDateTime <= endDate);
                });
            }

            return records;
        }


        public AUDIT_Record AddRecord(string actionName = null, long? userID = null)
        {
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                var action = AuditActionsRepository.GetSingle(x => x.AuditActionName == actionName);
                if (action != null)
                {
                    if (!userID.HasValue)
                    {
                        var UserId = ResourceOwnerData.GetUserId();
                        var cUser = UsersRepository.GetSingle(x => x.AspUserID == new Guid(Convert.ToString(UserId)));
                        userID = cUser?.UserID;
                    } 

                    var ClientId = ResourceOwnerData.GetClientId();
                    var cClient = AuthClientRepository.GetSingle(x => x.ClientId == new Guid(Convert.ToString(ClientId)));

                    AUDIT_Record entity = new AUDIT_Record()
                    {
                        AuditActionID = action?.AuditActionID,
                        UserID = userID,
                        PerformedDateTime = DateTime.UtcNow,
                        SoftwareVersion = cClient?.ClientName,
                    };

                    Add(entity);

                    return entity;
                }
            }

            return null;
        }

        public AUDIT_Record AddAuthRecord(Guid userId, string clientName, string actionName = null)
        {
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                var action = AuditActionsRepository.GetSingle(x => x.AuditActionName == actionName);
                if (action != null)
                {
                    var cUser = UsersRepository.GetSingle(x => x.AspUserID == new Guid(Convert.ToString(userId)));
                    AUDIT_Record entity = new AUDIT_Record()
                    {
                        AuditActionID = action?.AuditActionID,
                        UserID = cUser?.UserID,
                        PerformedDateTime = DateTime.UtcNow,
                        SoftwareVersion = clientName,
                    };
                    Add(entity);
                    return entity;
                }
            }

            return null;
        }

        #endregion
    }
}