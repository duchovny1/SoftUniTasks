﻿namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
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
            var hallsDtos = JsonConvert.DeserializeObject<ImportHallDto[]>(jsonString);

            List<Hall> hallsToAdd = new List<Hall>();
            StringBuilder sb = new StringBuilder();

            foreach (var hallDto in hallsDtos)
            {
                if (!IsValid(hallDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Hall hall = Mapper.Map<Hall>(hallDto);

                if (!IsValid(hall))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                hallsToAdd.Add(hall);

                for (int i = 0; i < hallDto.SeatsCount; i++)
                {
                    Seat seat = new Seat();
                    seat.HallId = hall.Id;

                    hall.Seats.Add(seat);
                }

                string projectionType;

                if (hallDto.Is3D && hallDto.Is4Dx)
                {
                    projectionType = "3D/4Dx";
                }
                else if (!hallDto.Is3D && !hallDto.Is4Dx)
                {
                    projectionType = "Normal";
                }
                else if (!hallDto.Is3D && hallDto.Is4Dx)
                {
                    projectionType = "4Dx";
                }
                else
                {
                    projectionType = "3D";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat,
                    hall.Name,
                    projectionType,
                    hall.Seats.Count));
            }


            context.Halls.AddRange(hallsToAdd);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportProjectionDto[]),
                new XmlRootAttribute("Projections"));

            var projectionsDto = (ImportProjectionDto[])serializer.Deserialize(new StringReader(xmlString));


            foreach (var projectionDto in projectionsDto)
            {
                if (!IsValid(projectionDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Movie movie = context.Movies.FirstOrDefault(x => x.Id == projectionDto.MovieId);

                if (movie == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Hall hall = context.Halls.FirstOrDefault(x => x.Id == projectionDto.HallId);

                if (hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Projection projection = new Projection
                {
                    MovieId = projectionDto.MovieId,
                    HallId = projectionDto.HallId,
                    DateTime = DateTime.ParseExact(projectionDto.DateTime, @"yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };

                context.Projections.Add(projection);
                sb.AppendLine(string.Format(SuccessfulImportProjection,
                    projection.Movie.Title,
                    projection.DateTime.ToString(@"MM/dd/yyyy")));
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportCustomersTickets[]),
                new XmlRootAttribute("Customers"));

            var customersWithTickets = (ImportCustomersTickets[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var dto in customersWithTickets)
            {
                if(!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Customer customer = new Customer
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    Balance = dto.Balance
                };


                List<Ticket> currentTickets = new List<Ticket>();

                foreach (var dtoTicket in dto.Tickets)
                {
                    bool isVaildDto = IsValid(dtoTicket);
                    bool isProjectionExist = context.Projections.Any(x => x.Id == dtoTicket.ProjectionId);

                    if(!isVaildDto || !isProjectionExist)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket ticket = new Ticket()
                    {
                        ProjectionId = dtoTicket.ProjectionId,
                        Price = dtoTicket.Price,
                        CustomerId = customer.Id
                    };

                    currentTickets.Add(ticket);
                }
                customer.Tickets = currentTickets;

                context.Tickets.AddRange(currentTickets);
                context.Customers.Add(customer);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket,
                    customer.FirstName,
                    customer.LastName,
                    customer.Tickets.Count()));
            }
           
            return sb.ToString().TrimEnd();
        }
    }
}