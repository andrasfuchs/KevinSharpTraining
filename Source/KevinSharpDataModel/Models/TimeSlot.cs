using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KevinSharp.DataModel
{
    public class TimeSlot
    {
        [Column(Order = 1)]
        public int TimeSlotId { get; set; }

        [Required]
        [Column(Order = 2)]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        [Column(Order = 3)]
        public int Duration { get; set; }

        public override string ToString()
        {
            return this.ToString("Central European Standard Time");
        }

        public string ToString(string timeZoneId = "Pacific Standard Time", bool appendTimeZone = true)
        {
            string dateFormat = "yyyy-MM-dd, dddd";
            string timeFormat = "HH:mm";
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime convertedStartTime = TimeZoneInfo.ConvertTimeFromUtc(StartTimeUtc, tzi);
            DateTime convertedEndTime = convertedStartTime + new TimeSpan(0, Duration, 0);
            
            return convertedStartTime.ToString(dateFormat) + " " + convertedStartTime.ToString(timeFormat) + " - " + convertedEndTime.ToString(timeFormat) + (appendTimeZone ? " (UTC" + tzi.BaseUtcOffset.Hours.ToString("0") + ")" : "");
        }

        public string ToDateString(string timeZoneId = "Pacific Standard Time")
        {
            string dateFormat = "yyyy-MM-dd";
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime convertedStartTime = TimeZoneInfo.ConvertTimeFromUtc(StartTimeUtc, tzi);

            return convertedStartTime.ToString(dateFormat);
        }    
    }
}
