namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var objects = context.Authors.Select(x => new ExportAuthorDto
            {
                AuthorName = x.FirstName + " " + x.LastName,
                Books = x.AuthorsBooks.Select(b => new ExportBookDto
                {
                    BookName = b.Book.Name,
                    BookPrice = b.Book.Price.ToString("F2")
                })
                .OrderByDescending(a => decimal.Parse(a.BookPrice))
                .ToArray()
            })
            .ToArray()
            .OrderByDescending(a => a.Books.Count())
            .ThenBy(n => n.AuthorName);

            var serializer = JsonConvert.SerializeObject(objects, Formatting.Indented);

            return serializer;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {

            var objects = context.Books.Where(x => x.PublishedOn < date && x.Genre == (Genre)3)
                .Select(x => new ExportOldestBooks
                {
                    Pages = x.Pages,
                    Name = x.Name,
                    Date = x.PublishedOn.ToString(@"MM/dd/yyyy")
                }).OrderByDescending(x => x.Pages)
                .ThenByDescending(x => x.Date)
                .Take(10)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportOldestBooks[]), new XmlRootAttribute("Books"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), objects, namespaces);


            return sb.ToString();
        }
    }
}