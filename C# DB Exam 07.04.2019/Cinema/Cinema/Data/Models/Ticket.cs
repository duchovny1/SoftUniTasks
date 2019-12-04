using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Data.Models
{
    public class Ticket
    {
        public Ticket()
        {
            Projections = new HashSet<Projection>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public int ProjectionId { get; set; }

        public ICollection<Projection> Projections { get; set; }

    }
}
