using CarDealer.Data;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System.IO;
using System.Xml.Serialization;
using AutoMapper;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<CarDealerProfile>();
            });

            using (var db = new CarDealerContext())
            {
                var inputXml = File.ReadAllText("./../../../Datasets/suppliers.xml");
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var rootElement = new XmlRootAttribute("Suppliers");

            var serializer = new XmlSerializer(typeof(DTO_ImportSuppliers[]), rootElement);

            using (var reader = new StringReader(inputXml))
            {
                var suppliers = (DTO_ImportSuppliers[])serializer.Deserialize(reader);

                foreach (var dto in suppliers)
                {
                    var supplier = Mapper.Map<Supplier>(dto);

                    context.Suppliers.Add(supplier);
                }


            }
            int count = context.SaveChanges();
            return $"Successfully imported {count}";
        }
    }
}