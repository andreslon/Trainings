using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories.Interface
{
    public interface IEquipmentRepository : IEntityBaseRepository<CONTACT_Equipment>
    {
        IQueryable<CONTACT_Equipment> GetAll(long? affiliationID, bool? isActive, string search);
    }
}
