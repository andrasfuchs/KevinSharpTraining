using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KevinSharp.DataModel
{
    public class TimeSlotGroup
    {
        [Column(Order = 1)]
        public int TimeSlotGroupId { get; set; }

        [Required]
        [Column(Order = 2)]
        public Course Course { get; set; }

        [Required]
        [Column(Order = 3)]
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }

        public override string ToString()
        {
            return this.ToString("Central European Standard Time");
        }

        public string ToString(string timeZoneId = "Pacific Standard Time")        
        {
            string result = "";
            if (TimeSlots.Count == 0) return result;

            foreach (TimeSlot ts in TimeSlots)
            {
                result += ts.ToString(timeZoneId) + " and ";
            }

            return result.Substring(0, result.Length - 5);
        }

        public string ToShortString(string timeZoneId = "Pacific Standard Time")
        {
            string result = "";
            if (TimeSlots.Count == 0) return result;

            foreach (TimeSlot ts in TimeSlots)
            {
                result += ts.ToDateString(timeZoneId) + ", ";
            }

            return result.Substring(0, result.Length - 2);
        }
    }
}
