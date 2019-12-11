using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
   public class ImportProjectionDto
    {
        [Required]
        [XmlElement(ElementName = "MovieId")]
        public int MovieId { get; set; }

        [Required]
        [XmlElement(ElementName = "HallId")]
        public int HallId { get; set; } 

        [Required]
        [XmlElement(ElementName = "DateTime")]
        public string DateTime { get; set; }
    }
}
