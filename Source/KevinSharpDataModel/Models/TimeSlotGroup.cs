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
        public string Code { get; set; }

        [Required]
        [Column(Order = 3)]
        public Course Course { get; set; }

        [Required]
        [Column(Order = 4)]
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }

        public void GenerateNewCode()
        {
            string dateCode = "0000";
            int timeSum = 0;
            foreach (TimeSlot ts in TimeSlots)
            {
                string dc = ts.StartTimeUtc.Month.ToString("00") + ts.StartTimeUtc.Day.ToString("00");
                
                if ((dateCode == "0000") || (dc.CompareTo(dateCode) < 0))
                {
                    dateCode = dc;
                }

                timeSum += ts.StartTimeUtc.Year + ts.StartTimeUtc.Hour;
            }

            this.Code = "T" + dateCode + (char)((int)'A' + timeSum % 26);
        }

        public override string ToString()
        {
            return this.ToString("Central European Standard Time");
        }

        public string ToString(string timeZoneId = "Pacific Standard Time")        
        {
            string result = "";
            if (TimeSlots.Count == 0) return result;

            List<TimeSlot> timeSlots = new List<TimeSlot>(TimeSlots);            
            for (int i = 0; i < timeSlots.Count; i++)
            {
                result += timeSlots[i].ToString(timeZoneId, i == timeSlots.Count - 1) + (i < timeSlots.Count - 1 ? " and " : ""); 
            }

            return result;
        }

        public string ToShortString(string timeZoneId = "Pacific Standard Time")
        {
            string result = "";
            if (TimeSlots.Count == 0) return result;

            List<TimeSlot> timeSlots = new List<TimeSlot>(TimeSlots);
            for (int i = 0; i < timeSlots.Count; i++)
            {
                result += timeSlots[i].ToDateString(timeZoneId) + (i < timeSlots.Count - 1 ? ", " : "");
            }

            return result;
        }
    }
}
