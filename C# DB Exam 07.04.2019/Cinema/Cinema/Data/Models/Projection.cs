using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Data.Models
{
    public class Projection
    {
        public Projection()
        {
            Tickets = new HashSet<Ticket>();
        }
        //•	MovieId – integer, foreign key(required)
        //•	Movie – the projection’s movie
        //•	HallId – integer, foreign key(required)
        //•	Hall – the projection’s hall 
        [Key]
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; } // must be setted to foreign key

        public Movie Movie { get; set; }

        [Required]
        public int HallId { get; set; } // must be set as a foreign key

        public Hall Hall { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
