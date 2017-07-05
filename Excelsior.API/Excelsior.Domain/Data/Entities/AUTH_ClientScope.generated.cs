using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    public partial class AUTH_ClientScope
    {
        private Guid _ClientScopeId;
        public virtual Guid ClientScopeId
        {
            get
            {
                return this._ClientScopeId;
            }
            set
            {
                this._ClientScopeId = value;
            }
        }

        private Guid _ScopeId;
        public virtual Guid ScopeId
        {
            get
            {
                return this._ScopeId;
            }
            set
            {
                this._ScopeId = value;
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

        private string _Name;
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
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

        private AUTH_Scope _AUTHScope;
        public virtual AUTH_Scope AUTHScope
        {
            get
            {
                return this._AUTHScope;
            }
            set
            {
                this._AUTHScope = value;
            }
        }
    }
}
