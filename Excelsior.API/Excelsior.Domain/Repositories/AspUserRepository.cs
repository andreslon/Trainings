using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class AspUserRepository : EntityBaseRepository<Aspnet_User>, IAspUserRepository
    {
        #region Constructor
        public AspUserRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions  
        public Aspnet_Application GetCurrentApplication()
        {
            return Context.Aspnet_Applications.FirstOrDefault();
        }
        public virtual void AddUser(Aspnet_User entity)
        {
            Context.Add(entity);
        }
        #endregion
    }
}
