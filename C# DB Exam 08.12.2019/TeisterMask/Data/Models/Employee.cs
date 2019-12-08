﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.Data.Models
{
    public class Employee
    {

        public Employee()
        {
            EmployeesTasks = new HashSet<EmployeeTask>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
        //^[0-9]{3}-[0-9]{3}-[0-9]{4}
        public string Phone { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
