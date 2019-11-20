using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new ProductShopContext())
            {
                //string usersJson = File.ReadAllText("./../../../Datasets/categories-products.json");

                Console.WriteLine(GetUsersWithProducts(db));
            }
        }

        //Problem 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {

            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //Problem 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //Problem 3
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {

            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                .Where(n => n.Name != null).ToList();


            context.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }


        //Problem 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {

            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }


        //Problem 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products.Where(x => x.Price >= 500 && x.Price <= 1000).
                Select(x => new
                {
                    name = x.Name,
                    price =  x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName// seller.NAME !

                })
                .OrderBy(x => x.price)
                .ToList();
           
            var exportJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            return exportJson;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var usersSoldItems = context.Users.Where(x => x.ProductsSold.Any(ps => ps.Buyer != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                    .Where(p => p.Buyer != null)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    }).ToList()
                }).OrderBy(u => u.lastName).ThenBy(u => u.firstName).ToList();

            var usersSoldProductsJson = JsonConvert.SerializeObject(usersSoldItems, Formatting.Indented);

            return usersSoldProductsJson;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var productsInCategories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(cp => new
                {
                    category = cp.Name,
                    productsCount = cp.CategoryProducts.Count(),
                    averagePrice = $"{cp.CategoryProducts.Average(x => x.Product.Price):f2}",
                    totalRevenue = $"{cp.CategoryProducts.Sum(x => x.Product.Price):f2}"
                })
               .ToList();

            var jsonProductsInCategories = JsonConvert.SerializeObject(productsInCategories);

            return jsonProductsInCategories;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersResult = context.Users.Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
            .OrderByDescending(x => x.ProductsSold.Where(ps => ps.Buyer != null).Count())
            .Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                age = u.Age,
                soldProducts = new
                {
                    count = u.ProductsSold.Where(ps => ps.Buyer != null).Count(),
                    products = u.ProductsSold.Where(ps => ps.Buyer != null).Select(ps => new
                    {
                        name = ps.Name,
                        price = ps.Price

                    }).ToList()
                }

            }).ToList();

            var resultJson = new
            {
                usersCount = usersResult.Count,
                users = usersResult
            };

            var output = JsonConvert.SerializeObject(resultJson, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return output;
        }

    }
}