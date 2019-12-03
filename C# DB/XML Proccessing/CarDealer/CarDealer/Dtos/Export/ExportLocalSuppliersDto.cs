using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Export
{
    [XmlType("supplier")]
    public class ExportLocalSuppliersDto
    {
        [XmlAttribute("id")]
        public int SupplierId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("parts-count")]
        public int PartsCount { get; set; }
    }
}
