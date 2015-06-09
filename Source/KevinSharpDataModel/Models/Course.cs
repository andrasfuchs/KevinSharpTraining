using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KevinSharp.DataModel
{
    public class Course
    {
        [Column(Order = 1)]
        public int CourseId { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Code { get; set; }

        [Required]
        [Column(Order = 3)]
        public string Name { get; set; }

        [Required]
        [Column(Order = 4)]
        public string ShortName { get; set; }

        [Required]
        [Column(Order = 5)]
        public string Description { get; set; }

        [Required]
        [Column(Order = 6)]
        public string[] Modules { get; set; }

        [Required]
        [Column(Order = 7)]
        public string Length { get; set; }

        [Required]
        [Column(Order = 8)]
        public CourseLevel Level { get; set; }

        [Required]
        [Column(Order = 9)]
        public virtual ICollection<TimeSlotGroup> TimeSlots { get; set; }
    }

    public enum CourseLevel { Beginner, Intermediate, Expert }
}
