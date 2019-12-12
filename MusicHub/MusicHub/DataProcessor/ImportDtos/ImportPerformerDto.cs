using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Performer")]
    public class ImportPerformerDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [Range(18, 80)]
        public int Age { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public ImportPeformerSongs[] PerformersSongs { get; set; }

    }
}
