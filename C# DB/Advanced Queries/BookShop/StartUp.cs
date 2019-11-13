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
    using System.Globalization;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                string genre = (Console.ReadLine());

                Console.WriteLine(GetBookTitlesContaining(db, genre));

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

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            List<string> categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            List<string> booksToOutput = new List<string>();



            foreach (var category in categories)
            {
                var books = context.BooksCategories.Where(x => x.Category.Name.ToLower() == category.ToLower())
                    .Select(b => new
                    {
                        b.Book.Title
                    }).ToList();

                foreach (var book in books)
                {
                    booksToOutput.Add(book.Title);
                }
            }

            booksToOutput = booksToOutput.OrderBy(x => x).ToList();
            string result = string.Join(Environment.NewLine, booksToOutput);
            return result;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {

            DateTime dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                }).Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var books = context.Books.Where(x => x.Author.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.Author.FirstName + " " + a.Author.LastName

                }).Distinct().OrderBy(x => x.FullName).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.FullName);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .Select(x => new
                {
                    x.Title

                }).OrderBy(t => t.Title).ToList();


            //var books = context.Books
            //    .Where(b => b.Title.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
            //    .Select(b => new
            //    {
            //        b.Title
            //    })
            //    .OrderBy(b => b.Title)
            //    .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var b in books)
            {
                sb.AppendLine(b.Title);
            }


            return sb.ToString().TrimEnd();
        }
    }
}
