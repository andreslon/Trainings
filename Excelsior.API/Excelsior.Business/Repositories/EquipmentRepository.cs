using Excelsior.Business.Logic;
using Excelsior.Domain;

namespace Excelsior.Business.Repositories
{
    public class EquipmentRepository
    {
        public DataModel db { get; set; }
        public EquipmentRepository(DataModel context)
        {
            db = context;
        }
        
        public int GetTotalCertifiedEquipmentByProcedure(long trialID, long? equipment, long? procedureID)
        {
            var count = 0;
            if (equipment.HasValue && procedureID.HasValue)
            {
                var handler = new EquipmentHandler(db);
                count = handler.GetTotalCertifiedEquipmentByProcedure(trialID, equipment.Value, procedureID.Value);
            }
            
            return count;
        }
    }
}