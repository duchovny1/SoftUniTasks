using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("Part")]
    public class DTO_ImportParts
    {
        [XmlElement("name")]

        public string Name { get; set; }


        [XmlElement("price")]

        public long Price { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [XmlElement("supplierId")]
        public int SupplierId { get; set; }
    }
}
