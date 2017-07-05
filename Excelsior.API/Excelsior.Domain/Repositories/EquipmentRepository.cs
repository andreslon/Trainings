using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class EquipmentRepository : EntityBaseRepository<CONTACT_Equipment>, IEquipmentRepository
    {
        #region Constructor
        public EquipmentRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions

        public IQueryable<CONTACT_Equipment> GetAll(long? affiliationID, bool? isActive, string search)
        {
            var query = GetAll();
            if (affiliationID.HasValue)
            {
                query = query.Where(x => x.AffiliationID == affiliationID.Value);
            }
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                //query = query.Where(x => x.MainSerialNum.Contains(search)
                //    || x.Notes.Contains(search)
                //    || x.SeconarySerialNum.Contains(search)
                //    || x.SoftwareVersion.Contains(search)
                //    || x.CONTACTEquipmentModel.EquipmentType.Contains(search)
                //    || x.CONTACTEquipmentModel.ManufacturerModel.Contains(search)
                //    || x.CONTACTEquipmentModel.ManufacturerName.Contains(search)
                //    || x.CONTACTAffiliation.AffiliationName.Contains(search)
                //    || (x.CONTACTAffiliation.CONTACTCountry == null ? false : x.CONTACTAffiliation.CONTACTCountry.CountryName.Contains(search)));

                query = from equipment in query
                        join affiliation in Context.CONTACT_Affiliations on equipment.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        where equipment.MainSerialNum.Contains(search)
                            || equipment.Notes.Contains(search)
                            || equipment.SeconarySerialNum.Contains(search)
                            || equipment.SoftwareVersion.Contains(search)
                            || equipment.CONTACTEquipmentModel.EquipmentType.Contains(search)
                            || equipment.CONTACTEquipmentModel.ManufacturerModel.Contains(search)
                            || equipment.CONTACTEquipmentModel.ManufacturerName.Contains(search)
                            || affiliation.AffiliationName.Contains(search)
                            || (country != null ? country.CountryName.Contains(search) : false)
                        select equipment;
            }
            return query;
        }
        #endregion
    }
}
