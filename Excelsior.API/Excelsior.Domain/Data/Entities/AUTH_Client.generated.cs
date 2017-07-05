using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    public partial class AUTH_Client
    {
        private Guid _ClientId;
        public virtual Guid ClientId
        {
            get
            {
                return this._ClientId;
            }
            set
            {
                this._ClientId = value;
            }
        }

        private string _ClientName;
        public virtual string ClientName
        {
            get
            {
                return this._ClientName;
            }
            set
            {
                this._ClientName = value;
            }
        }

        private string _RedirectUri;
        public virtual string RedirectUri
        {
            get
            {
                return this._RedirectUri;
            }
            set
            {
                this._RedirectUri = value;
            }
        }

        private Guid _UserId;
        public virtual Guid UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }

        private Aspnet_User _ApnetUser;
        public virtual Aspnet_User ApnetUser
        {
            get
            {
                return this._ApnetUser;
            }
            set
            {
                this._ApnetUser = value;
            }
        }

        private IList<AUTH_ClientScope> _AUTHClientScopes = new List<AUTH_ClientScope>();
        public virtual IList<AUTH_ClientScope> AUTH_ClientScopes
        {
            get
            {
                return this._AUTHClientScopes;
            }
        }

        private IList<AUTH_ClientSecret> _AUTHClientSecrets = new List<AUTH_ClientSecret>();
        public virtual IList<AUTH_ClientSecret> AUTH_ClientSecrets
        {
            get
            {
                return this._AUTHClientSecrets;
            }
        }
    }
}
