using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities
{
    public class SeriesGroupRequestDto
    {
        [Required]
        public IList<long> SeriesIds { get; set; }
    }
}