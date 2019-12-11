namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
            .Where(x => x.Rating >= rating &&
            x.Projections.Any(p => p.Tickets.Count > 0))
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.Projections.Sum(t => t.Tickets.Sum(p => p.Price)))
                .Take(10)
                .Select(x => new
                {
                    MovieName = x.Title,
                    Rating = x.Rating.ToString("F2"),
                    TotalIncomes = x.Projections.Sum(t => t.Tickets.Sum(p => p.Price)).ToString("f2"),
                    Customers = x.Projections
                    .SelectMany(o => o.Tickets)
                    .Select(t => new 
                    {
                        FirstName = t.Customer.FirstName,
                        LastName = t.Customer.LastName,
                        Balance = t.Customer.Balance.ToString("F2")
                    })
                    .OrderByDescending(t => t.Balance)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .ToArray()
                    
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers.Where(x => x.Age >= age)
                .OrderByDescending(x => x.Tickets.Sum(p => p.Price))
                .Take(10)
                .Select(x => new ExportTopCustomersDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SpentMoney = x.Tickets.Sum(p => p.Price).ToString("F2"),
                    SpentTime = TimeSpan.FromMilliseconds(x.Tickets
                    .Sum(t => t.Projection.Movie.Duration.TotalMilliseconds)).ToString(@"hh\:mm\:ss")
                })
                .ToArray();

            var root = new XmlRootAttribute("Customers");

            StringBuilder sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportTopCustomersDto[]), root);

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}