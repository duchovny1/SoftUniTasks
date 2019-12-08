using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
   public class ImportProjectionDto
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int HallId { get; set; } 

        [Required]
        public DateTime DateTime { get; set; }
    }
}
