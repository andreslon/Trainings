using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Excelsior.Business.Helpers
{
    public class MultiModalityWarning
    {
        public MultiModalityWarning()
        {
            Procedures = new List<MultiModalityProcedure>();
        }
        public List<MultiModalityProcedure> Procedures { get; set; }
    }

    public class MultiModalityProcedure
    {
        public string Name { get; set; }
        public bool IsMissing { get; set; }
        public bool IsReceivedLateralitySet { get; set; }
        public bool IsImagesMissing { get; set; }
        public bool IsImagesProcessed { get; set; }
        public bool IsImagesLateralitySet { get; set; }
        public bool IsImagesCalibrationSet { get; set; }
    }
}