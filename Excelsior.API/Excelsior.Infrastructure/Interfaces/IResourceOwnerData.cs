using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Infrastructure.Interfaces
{
    public interface IResourceOwnerData
    {
        string GetUserId();
        string GetClientId();
    }
}
