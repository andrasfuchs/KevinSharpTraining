using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KevinSharp.DataModel
{
    public class Applicant
    {
        [Column(Order = 1)]
        public int ApplicantId { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Email { get; set; }

        [Required]
        [Column(Order = 3)]
        public Course Course { get; set; }

        [Required]
        [Column(Order = 4)]
        public string TimeSlotGroupCode { get; set; }

        [Column(Order = 5)]
        public string Notes { get; set; }
    }
}
