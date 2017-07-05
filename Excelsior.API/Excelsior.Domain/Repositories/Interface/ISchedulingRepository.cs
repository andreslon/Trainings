using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISchedulingRepository
    {
        IQueryable<CERT_ImgProcedureList> GetProcedures(long trialId, bool? scheduled, string search);
        IQueryable<PACS_TimePointsList> GetTimePointsList(long trialId);
    }
}