using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        //        •	Id - integer, Primary Key
        //•	Name - text with length[2, 40] (required)
        //•	OpenDate - date and time(required)
        //•	DueDate - date and time(can be null)
        //•	Tasks - collection of type Task
        public Project()
        {
            Tasks = new HashSet<Task>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }
        // it might need to be string !

        public string DueDate { get; set; } // it might need to be string !

        public ICollection<Task> Tasks { get; set; }


    }
}
