using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class EmailBaseDto
    {
        [StringLength(100)]
        [Required]
        public string From { get; set; }
        [StringLength(100)]
        [Required]
        public string subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
