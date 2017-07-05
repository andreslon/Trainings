using Excelsior.Domain;
using System.Linq;

namespace Excelsior.Business.Logic
{
    public class FramesHandler
    {
        public DataModel db { get; set; }
        public FramesHandler(DataModel context)
        {
            db = context;
        }

        public IQueryable<PACS_DicomFrame> GetFrames(long rawDataID)
        {
            var frames = db.PACS_DicomFrames
                .Where(item => item.IsActive && item.RawDataID == rawDataID).OrderBy(item => item.FrameIndex);
            return frames;
        }
    }
}
