using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{

    [XmlType("Ticket")]
    public class ImportTicketDto
    {
        [XmlElement("ProjectionId")]
        [Required]
        public int ProjectionId { get; set; }


        [XmlElement("Price")]
        [Required]
        [Range(typeof(decimal), "0.01", "100000000000")]
        public decimal Price { get; set; }
    }
}
