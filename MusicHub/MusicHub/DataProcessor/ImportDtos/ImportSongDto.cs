using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class ImportSongDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        public string Duration { get; set; }
        public string CreatedOn { get; set; }
        public string Genre { get; set; }
        public int AlbumId { get; set; }

        public int WriterId { get; set; }

        [Required]
        [Range(0.00, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
