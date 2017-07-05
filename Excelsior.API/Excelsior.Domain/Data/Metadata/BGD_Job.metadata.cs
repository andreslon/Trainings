using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(BGD_Job.BGD_JobMetadata))]
    public partial class BGD_Job
    {
        internal sealed class BGD_JobMetadata
        {
            [Association("BGD_JobStatus_Association", "JobStatusID", "JobStatusID")]
            public BGD_JobStatus BGDJobStatus
            {
                get;
                set;
            }            
        }
    }
}