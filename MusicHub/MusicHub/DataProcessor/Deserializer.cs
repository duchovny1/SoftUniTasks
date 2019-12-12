namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writers = JsonConvert.DeserializeObject<ImportWritersDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            foreach (var dto in writers)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Writer writer = new Writer()
                {
                    Name = dto.Name,
                    Pseudonym = dto.Pseudonym
                };

                context.Writers.Add(writer);
                sb.AppendLine(string.Format(SuccessfullyImportedWriter,
                    writer.Name));
            }

            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerAlbums = JsonConvert.DeserializeObject<ImportProducersAlbums[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            foreach (var dto in producerAlbums)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Producer producer = new Producer
                {
                    Name = dto.Name,
                    Pseudonym = dto.Pseudonym,
                    PhoneNumber = dto.PhoneNumber == null ? null : dto.PhoneNumber
                };


                bool areValidAlbums = true;
                List<Album> albums = new List<Album>();

                foreach (var albumDto in dto.Albums)
                {
                    if (!IsValid(albumDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        areValidAlbums = false;
                        break; ;
                    }

                    Album album = new Album
                    {
                        Name = albumDto.Name,
                        ReleaseDate = DateTime.ParseExact(albumDto.ReleaseDate, @"dd/MM/yyyy",
                        CultureInfo.InvariantCulture)
                    };

                    albums.Add(album);
                    context.Albums.Add(album);
                    context.SaveChanges();
                }

                if (areValidAlbums)
                {

                    producer.Albums = albums;
                    context.Producers.Add(producer);
                    context.SaveChanges();

                    if (string.IsNullOrEmpty(dto.PhoneNumber))
                    {
                        sb.AppendLine(String.Format(SuccessfullyImportedProducerWithNoPhone,
                            producer.Name,
                            albums.Count));
                    }
                    else
                    {
                        sb.AppendLine(String.Format(SuccessfullyImportedProducerWithPhone,
                            producer.Name,
                            producer.PhoneNumber,
                            albums.Count));
                    }
                }


            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportSongDto[]),
                new XmlRootAttribute("Songs"));

            var objectsDto = (ImportSongDto[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var dto in objectsDto)
            {
                bool isValid = IsValid(dto);
                bool isGenreValid = Enum.IsDefined(typeof(Genre), dto.Genre);
                bool isAlbumExists = context.Albums.Any(x => x.Id == dto.AlbumId);
                bool isWriterExists = context.Writers.Any(x => x.Id == dto.WriterId);



                if (isValid && isGenreValid
                    && isAlbumExists && isWriterExists)
                {
                    Song song = new Song
                    {
                        Name = dto.Name,
                        Duration = TimeSpan.ParseExact(dto.Duration, @"c", CultureInfo.InvariantCulture),
                        CreatedOn = DateTime.ParseExact(dto.CreatedOn, @"dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre),
                        AlbumId = dto.AlbumId,
                        WriterId = dto.WriterId,
                        Price = dto.Price
                    };

                    context.Songs.Add(song);
                    context.SaveChanges();
                    sb.AppendLine(string.Format(SuccessfullyImportedSong,
                        song.Name,
                        song.Genre.ToString(),
                        song.Duration.ToString()));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportPerformerDto[]),
                new XmlRootAttribute("Performers"));

            var objectsDto = (ImportPerformerDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            foreach (var dto in objectsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Performer performer = new Performer
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    NetWorth = dto.NetWorth
                };

                List<SongPerformer> songsByPerformer = new List<SongPerformer>();

                bool shouldAddPerformer = true;

                foreach (var currentSong in dto.PerformersSongs)
                {

                    bool isSongValid = context.Songs.Any(x => x.Id == currentSong.Id);

                    if (!isSongValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        shouldAddPerformer = false;
                        break;
                    }

                    SongPerformer songPerformer = new SongPerformer
                    {
                        SongId = currentSong.Id,
                        PerformerId = performer.Id
                    };

                    songsByPerformer.Add(songPerformer);
                    context.SongsPerformers.Add(songPerformer);
                }

                if (shouldAddPerformer)
                {
                    performer.PerformerSongs = songsByPerformer;
                    context.Performers.Add(performer);
                    context.SaveChanges();
                    sb.AppendLine(String.Format(SuccessfullyImportedPerformer,
                        performer.FirstName,
                        performer.PerformerSongs.Count));

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