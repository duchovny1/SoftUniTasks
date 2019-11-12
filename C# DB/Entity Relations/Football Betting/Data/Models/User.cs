using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{
    
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }


        [MaxLength(30)]
        [Required]
        public string Password { get; set; }

        [MaxLength(30)]
        [Required]
        public string Username { get; set; }

        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}
