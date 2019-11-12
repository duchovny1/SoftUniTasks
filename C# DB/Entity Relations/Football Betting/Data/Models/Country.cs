using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{

    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [MaxLength(30)]

        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; } = new HashSet<Town>(); //many towns in one contry
    }
}
