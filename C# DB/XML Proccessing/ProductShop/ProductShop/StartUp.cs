namespace ProductShop
{
    using System;
    using ProductShop.Data;
    using ProductShop.Dtos.Import;
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using ProductShop.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ProductShopProfile>();
            });


            using (var db = new ProductShopContext())
            {
                string categories = File.ReadAllText("./../../../Datasets/categories.xml");

                System.Console.WriteLine(ImportCategories(db, categories));


            }

        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var root = new XmlRootAttribute("Users");
            var xmlSerializer = new XmlSerializer(typeof(DtoUser[]), root);

            var usersDtos = (DtoUser[])xmlSerializer.Deserialize(new StringReader(inputXml));

            foreach (var userDto in usersDtos)
            {
                var user = Mapper.Map<User>(userDto);
                context.Users.Add(user);
            }

            context.SaveChanges();


            return $"Successfully imported {usersDtos.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var rootAttribute = new XmlRootAttribute("Products");
            var xmlSerializer = new XmlSerializer(typeof(DtoProduct[]), rootAttribute);

            var productsDto = (DtoProduct[])xmlSerializer.Deserialize(new StringReader(inputXml));

            foreach (var productDto in productsDto)
            {
                var product = Mapper.Map<Product>(productDto);
                context.Products.Add(product);
            }

            int productsCount = context.SaveChanges();

            return $"Successfully imported {productsCount}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var rootElement = new XmlRootAttribute("Categories");

            var serializer = new XmlSerializer(typeof(DtoCategories[]), rootElement);

            var categoriesDto = (DtoCategories[])serializer.Deserialize(new StringReader(inputXml))
               ;

            List<Category> categories = new List<Category>();

            foreach (var ctDto in categoriesDto)
            {
                if (!string.IsNullOrEmpty(ctDto.Name))
                {
                    var category = Mapper.Map<Category>(ctDto);

                    categories.Add(category);
                }

            }


            context.Categories.AddRange(categories);

            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }

}