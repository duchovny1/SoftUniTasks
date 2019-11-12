using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace P03_FootballBetting.Data.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        public bool IsInjured { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        public int PositionId { get; set; }

        public Position Position { get; set; }

        [Required]
        public byte SquadNumber { get; set; }// it might need to be int


        public int TeamId { get; set; }

        public Team Team { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new HashSet<PlayerStatistic>();
    }
}
