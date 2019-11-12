namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Town
    {
        public int TownId { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }


        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();


    }
}
