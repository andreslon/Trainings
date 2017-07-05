using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    public partial class AUTH_Scope
    {
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

        private string _DisplayName;
        public virtual string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                this._DisplayName = value;
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

        private IList<AUTH_ClientScope> _AUTHClientScopes = new List<AUTH_ClientScope>();
        public virtual IList<AUTH_ClientScope> AUTH_ClientScopes
        {
            get
            {
                return this._AUTHClientScopes;
            }
        }

        private IList<AUTH_ScopeClaim> _AUTHScopeClaims = new List<AUTH_ScopeClaim>();
        public virtual IList<AUTH_ScopeClaim> AUTH_ScopeClaims
        {
            get
            {
                return this._AUTHScopeClaims;
            }
        }
    }
}
