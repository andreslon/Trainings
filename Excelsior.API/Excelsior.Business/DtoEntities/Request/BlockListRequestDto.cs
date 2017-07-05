using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities
{
    public class BlockListRequestDto
    {
        [Required]
        public string OriginalFileName { get; set; }
        [Required]
        public IList<string> BlockList { get; set; }
    }
}