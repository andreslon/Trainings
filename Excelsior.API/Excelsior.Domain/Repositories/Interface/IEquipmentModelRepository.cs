using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories.Interface
{
    public interface IEquipmentModelRepository : IEntityBaseRepository<CONTACT_EquipmentModel>
    {
        IQueryable<CONTACT_EquipmentModel> GetAll(string search);
    }
}
