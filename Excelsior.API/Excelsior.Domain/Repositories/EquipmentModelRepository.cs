using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class EquipmentModelRepository : EntityBaseRepository<CONTACT_EquipmentModel>, IEquipmentModelRepository
    {
        #region Constructor
        public EquipmentModelRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions

        public IQueryable<CONTACT_EquipmentModel> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.EquipmentType.Contains(search)
                    || x.ManufacturerModel.Contains(search)
                    || x.ManufacturerName.Contains(search));
            }
            return query;
        }
        #endregion
    }
}
