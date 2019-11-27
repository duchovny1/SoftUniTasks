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
    using System.Text;
    using System.Xml;
    using ProductShop.Dtos.Export;

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

        //problem 01
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

        //problem 02
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

        //problem 03
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

        //problem 04

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var rootAttribute = new XmlRootAttribute("CategoryProducts");
            var xmlSerializer = new XmlSerializer(typeof(DtoCategoryProducts[]), rootAttribute);

            using (var stringReader = new StringReader(inputXml))
            {
                var categoryProducts = (DtoCategoryProducts[])xmlSerializer.Deserialize(stringReader);

                foreach (var categoryDto in categoryProducts)
                {
                    var category = context.Categories
                        .FirstOrDefault(c => c.Id == categoryDto.CategoryId);
                    var product = context.Products
                        .FirstOrDefault(p => p.Id == categoryDto.ProductId);

                    if (category != null && product != null)
                    {
                        var categoryProduct = Mapper.Map<CategoryProduct>(categoryDto);

                        context.CategoryProducts.Add(categoryProduct);
                    }
                }


            }



            int categoryProductsCount = context.SaveChanges();

            return $"Successfully imported {categoryProductsCount}";
        }


        //problem 05

        public static string GetProductsInRange(ProductShopContext context)
        {

            var products = context.Products.Where(p => p.Price >= 500 && p.Price <= 1000).OrderBy(x => x.Price)
               .Select(s => new DtoProductsInRange
               {
                   Name = s.Name,
                   Price = s.Price,
                   Buyer = s.Buyer.FirstName + " " + s.Buyer.LastName
               }).Take(10).ToArray();

            var root = new XmlRootAttribute("Products");

            var serializer = new XmlSerializer(typeof(DtoProductsInRange[]), root);

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });


            using (var writer = new StringWriter(sb))
            {

                serializer.Serialize(writer, products, namespaces);
            }


            return sb.ToString().TrimEnd();
        }



    }

}