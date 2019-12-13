namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportBookDto[]),
                new XmlRootAttribute("Books"));

            var objects = (ImportBookDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            foreach (var dto in objects)
            {
                bool isBookDefined = Enum.IsDefined(typeof(Genre), int.Parse(dto.Genre));

                if (!IsValid(dto) || !isBookDefined)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Book book = new Book
                {
                    Name = dto.Name,
                    Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre),
                    Price = dto.Price,
                    Pages = dto.Pages,
                    PublishedOn = DateTime.ParseExact(dto.PublishedOn, @"MM/dd/yyyy", CultureInfo.InvariantCulture)
                };


                context.Books.Add(book);

                sb.AppendLine(string.Format(SuccessfullyImportedBook,
                    book.Name, book.Price));
           }

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var objects = JsonConvert.DeserializeObject<ImportAuthorDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            foreach (var obj in objects)
            {
                bool isEmailExists = context.Authors.Any(x => x.Email == obj.Email);

                if (!IsValid(obj) || isEmailExists)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Author author = new Author
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Phone = obj.Phone,
                    Email = obj.Email
                };

                List<AuthorBook> authorBooks = new List<AuthorBook>();

                foreach (var book in obj.Books)
                {
                    if(book.Id == null)
                    {
                        continue;
                    }
                    bool isTheBookExists = context.Books.Any(x => x.Id == int.Parse(book.Id));

                    if (!isTheBookExists)
                    {
                        continue;
                    }

                    AuthorBook authorBook = new AuthorBook
                    {
                        AuthorId = author.Id,
                        BookId = int.Parse(book.Id)
                    };

                    authorBooks.Add(authorBook);
                    context.AuthorsBooks.Add(authorBook);
                }


                if(authorBooks.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                }
                else
                {
                    author.AuthorsBooks = authorBooks;
                    context.Authors.Add(author);
                    sb.AppendLine(string.Format(SuccessfullyImportedAuthor,
                        author.FirstName + " " + author.LastName,
                        author.AuthorsBooks.Count()));

                    context.SaveChanges();
                }
            }

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}