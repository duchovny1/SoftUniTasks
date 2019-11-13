namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                int year = int.Parse(Console.ReadLine());

                Console.WriteLine(GetBooksNotReleasedIn(db, year));

            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var bookTitles = context.Books.Where(x => (x.AgeRestriction.ToString()).ToLower() == command.ToLower())
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();

            string result = string.Join(Environment.NewLine, bookTitles);
            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(x => x.EditionType.ToString() == "Gold")
                .Where(x => x.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title).ToList();

            string result = string.Join(Environment.NewLine, goldenBooks);

            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price).ToList();


            foreach (var b in books)
            {
                sb.AppendLine($"{b.Title} - ${b.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                .Select(x => new
                {
                    x.BookId,
                    x.Title,
                    x.ReleaseDate
                })
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .ToList();


            foreach (var bookTitle in books)
            {
                sb.AppendLine(bookTitle.Title);
            }

            return sb.ToString().TrimEnd();
        }

    }
}
