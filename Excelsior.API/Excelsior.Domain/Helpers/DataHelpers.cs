using Excelsior.Infrastructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;

namespace Excelsior.Domain.Helpers
{
    public class DataHelpers
    {
        private static RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy> _RetryPolicy;
        public static RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy> RetryPolicy
        {
            get
            {
                if (_RetryPolicy == null)
                {
                    var setting = new Settings();
                    _RetryPolicy = new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(Convert.ToInt32(setting.GetSetting("SQLTransientErrorRetryCount")));
                }
                return _RetryPolicy;
            }
        }

    }
}
