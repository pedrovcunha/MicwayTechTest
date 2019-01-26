using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicwayTechTest.Models
{
    public partial class Driver
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
