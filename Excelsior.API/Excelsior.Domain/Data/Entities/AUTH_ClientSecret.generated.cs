using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    public partial class AUTH_ClientSecret
    {
        private Guid _ClientSecretId;
        public virtual Guid ClientSecretId
        {
            get
            {
                return this._ClientSecretId;
            }
            set
            {
                this._ClientSecretId = value;
            }
        }

        private string _ClientSecret;
        public virtual string ClientSecret
        {
            get
            {
                return this._ClientSecret;
            }
            set
            {
                this._ClientSecret = value;
            }
        }

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

        private AUTH_Client _AUTHClient;
        public virtual AUTH_Client AUTHClient
        {
            get
            {
                return this._AUTHClient;
            }
            set
            {
                this._AUTHClient = value;
            }
        }
    }
}
