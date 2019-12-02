namespace CarDealer
{
    using System;
    using CarDealer.Data;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using System.IO;
    using System.Xml.Serialization;
    using AutoMapper;

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
                var inputXml = File.ReadAllText("./../../../Datasets/cars.xml");
                ImportCars(db, inputXml);
            }
        }

        //problem 09
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


        // problem 10
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var rootElement = new XmlRootAttribute("Cars");

            var serializer = new XmlSerializer(typeof(DTO_ImportCars[]), rootElement);

            using (var reader = new StringReader(inputXml))
            {
                var cars = (DTO_ImportCars[])serializer.Deserialize(reader);

                foreach (var dto in cars)
                {
                    var car = Mapper.Map<Car>(dto);

                    context.Cars.Add(car);
                }
            }
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}