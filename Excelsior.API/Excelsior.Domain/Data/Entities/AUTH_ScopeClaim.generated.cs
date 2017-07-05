using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    public partial class AUTH_ScopeClaim
    {
        private Guid _ScopeClaimId;
        public virtual Guid ScopeClaimId
        {
            get
            {
                return this._ScopeClaimId;
            }
            set
            {
                this._ScopeClaimId = value;
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

        private string _Description;
        public virtual string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        private bool _AlwaysIncludeInIdToken;
        public virtual bool AlwaysIncludeInIdToken
        {
            get
            {
                return this._AlwaysIncludeInIdToken;
            }
            set
            {
                this._AlwaysIncludeInIdToken = value;
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
