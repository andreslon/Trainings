#pragma warning disable 1591
using Excelsior.Infrastructure.Interfaces;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace Excelsior.Domain
{
    public partial class DataModel : DataModelBase, IDataModelUnitOfWork
	{
        public DataModel()
            : base(connectionString, GetBackendConfiguration(), metadataSource)
        {
        }

        public DataModel(ISettings settings) :
            this(settings.GetConnectionString("EyeKorConnection"), GetBackendConfiguration(settings.GetSetting("SQLBackendType")), metadataSource)
        {

        }

        public DataModel(string connection)
            : base(connection, GetBackendConfiguration(), metadataSource)
        {
        }

        public DataModel(BackendConfiguration backendConfiguration)
            : base(connectionString, backendConfiguration, metadataSource)
        {
        }

        public DataModel(string connection, MetadataSource metadataSource)
            : base(connection, GetBackendConfiguration(), metadataSource)
        {
        }

        public DataModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
            : base(connection, backendConfiguration, metadataSource)
        {
        }

        public static BackendConfiguration GetBackendConfiguration(string backendType = "MsSql")
		{
			BackendConfiguration backend = new BackendConfiguration();
            //"azure"
            backend.Backend = backendType;
			backend.ProviderName = "System.Data.SqlClient";
            backend.Logging.MetricStoreSnapshotInterval = 0;

            backend.Runtime.CommandTimeout = 120;

            CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}

        /// <summary>
        /// Allows you to customize the BackendConfiguration of DataModel.
        /// </summary>
        /// <param name="config">The BackendConfiguration of DataModel.</param>
        static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);	
	}
}
#pragma warning restore 1591
