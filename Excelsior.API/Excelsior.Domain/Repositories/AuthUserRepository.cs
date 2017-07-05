using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class AuthUserRepository : EntityBaseRepository<Aspnet_Membership>, IAuthUserRepository
    {
        #region Constructor
        public AuthUserRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 
        public Aspnet_Membership GetByUserName(string userName)
        {
            return GetSingle(x => x.UserName == userName);
        }

        public IQueryable<Aspnet_Membership> GetByEmail(string email)
        {
            return FindBy(x => x.Email.ToLower() == email.ToLower());
        }

        public void ExecuteQuery(string commandText, Dictionary<string, object> parameters)
        {
            IDbConnection connection = this.Context.Connection;
            // 2. Create a new command.
            IDbCommand command = connection.CreateCommand();

            // 3. Initialize the CommandText property.
            command.CommandText = commandText;
            // 4. Create command parameters.
            foreach (var parameter in parameters)
            {
                IDbDataParameter dataParameter = command.CreateParameter();
                dataParameter.ParameterName = parameter.Key;
                dataParameter.Value = parameter.Value;
                command.Parameters.Add(dataParameter);
            }
            // 5. Invoke the ExecuteNonQuery method of the command object.
            int rowsAffected = command.ExecuteNonQuery();
            // 6. Invoke the SaveChanges method of the OpenAccessContext.

        }

        #endregion
    }
}
