namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            Movie[] movies = JsonConvert.DeserializeObject<Movie[]>(jsonString);

            List<Movie> moviesToAdd = new List<Movie>();

            foreach (var movie in movies)
            {
                if (!IsValid(movie))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var currentMovie = Mapper.Map<ImportMovieDto>(movie);
                moviesToAdd.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title,
                    movie.Genre, movie.Rating.ToString("F2")));
            }
            ;
            context.Movies.AddRange(moviesToAdd);
            context.SaveChanges();

            return sb.ToString().Trim();
            //IsValid(object obj);

        }

        private static bool IsValid(object obj)
        {
            var validator = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);

            return result;

        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallsDto = JsonConvert.DeserializeObject<ImportHallDto[]>(jsonString);

            List<Hall> halls = new List<Hall>();

            StringBuilder sb = new StringBuilder();

            foreach (var hallDto in hallsDto)
            {
                bool isValidDto = IsValid(hallDto);
                Hall hall = Mapper.Map<Hall>(hallDto);
                bool isValidHall = IsValid(hall);

                if (isValidDto == false || isValidHall == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                for (int i = 0; i < hallDto.SeatsCount; i++)
                {
                    hall.Seats.Add(new Seat());
                }

                string projectionType = string.Empty;

                if (hall.Is3D == true && hall.Is4Dx == true)
                {
                    projectionType = "4Dx/3D";
                }
                else if (hall.Is4Dx == true)
                {
                    projectionType = "4Dx";
                }
                else if (hall.Is3D == true)
                {
                    projectionType = "3D";
                }
                else
                {
                    projectionType = "Normal";
                }

                halls.Add(hall);

                sb.AppendLine(string.Format(SuccessfulImportHallSeat,
                    hall.Name,
                    projectionType,
                    hall.Seats.Count));
            }

            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
    }
}