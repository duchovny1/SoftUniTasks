namespace CarDealer
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarDealer.Data;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main(string[] args)
        {


            Mapper.Initialize(cfg =>
            cfg.AddProfile(new CarDealerProfile()));

            using (var db = new CarDealerContext())
            {


                // string suppliers = File.ReadAllText("./../../../Datasets/suppliers.xml");
                string parts = File.ReadAllText("./../../../Datasets/parts.xml");
                //string cars = File.ReadAllText("./../../../Datasets/cars.xml");
                //string customers = File.ReadAllText("./../../../Datasets/customers.xml");
                //string sales = File.ReadAllText("./../../../Datasets/sales.xml");

                Console.WriteLine(GetCarsWithTheirListOfParts(db));
            }

        }

        //problem 09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var rootElement = new XmlRootAttribute("Suppliers");

            var serializer = new XmlSerializer(typeof(ImportSupplierDto[]), rootElement);

            using (var reader = new StringReader(inputXml))
            {
                var suppliers = (ImportSupplierDto [])serializer.Deserialize(reader);

                foreach (var dto in suppliers)
                {
                    var supplier = Mapper.Map<Supplier>(dto);

                    context.Suppliers.Add(supplier);
                }


            }
            int count = context.SaveChanges();
            return $"Successfully imported {count}";
        }


        //problem 10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(
                typeof(ImportPartDto[]),
                new XmlRootAttribute("Parts"));

            var partsDtos = ((ImportPartDto[])serializer.Deserialize(new StringReader(inputXml)))
                .Where(x => context.Suppliers.Any(s => x.SuppliedId == s.Id))
                .ToArray();

            var parts = Mapper.Map<Part[]>(partsDtos);
            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        //problem 15
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .ProjectTo<ExportLocalSuppliersDto>()
                .ToArray();


            var serializer = new XmlSerializer(
                typeof(ExportLocalSuppliersDto[]),
                new XmlRootAttribute("suppliers"));

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, suppliers, namespaces);
            }

            return sb.ToString().TrimEnd();

        }

        //problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var cars = context
                .Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<ExportCarsWithParts>()
                .ToArray();

            foreach (var car in cars)
            {
                car.Parts = car.Parts
                    .OrderByDescending(p => p.Price)
                    .ToArray();
            }

            var serializer = new XmlSerializer(typeof(ExportCarsWithParts[]),
                new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, cars, namespaces);

            }

            return sb.ToString().TrimEnd();
        }

    }
}