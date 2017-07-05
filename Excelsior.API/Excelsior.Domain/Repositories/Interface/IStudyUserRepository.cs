using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories.Interface
{ 
    public interface IStudyUserRepository : IEntityBaseRepository<CONTACT_UserTrial>
    {
        IQueryable<CONTACT_UserTrial> GetAll(string userId, bool? isActive);
    }
}
