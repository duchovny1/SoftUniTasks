using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        public decimal Budget { get; set; }

        [Required]
        [MaxLength(5)]
        public string Initials { get; set; }

        public string LogoUrl { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }


        [Required]

        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }


        [Required]
        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Game> HomeGames { get; set; } = new HashSet<Game>();

        public ICollection<Game> AwayGames { get; set; } = new HashSet<Game>();

        public ICollection<Player> Players { get; set; } = new HashSet<Player>();




    }
}
