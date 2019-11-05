using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public int PatientId { get; set; }


        [Required]
        [MaxLength(50)]

        public string FirstName { get; set; }


        [Required]
        [MaxLength(50)]

        public string LastName { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public string Email { get; set; }

        [Required]
        public bool HasInsurance { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }
        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();

    }
}
