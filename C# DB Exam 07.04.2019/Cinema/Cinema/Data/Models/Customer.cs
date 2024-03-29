﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Data.Models
{
    public class Customer
    {

        //•	Balance - decimal (non-negative, minimum value: 0.01) (required)
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [Range(12,110)]
        public int Age { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "100000000000")]
        public decimal Balance { get; set; }

        public ICollection<Ticket> Tickets { get; set; }


    }
}
