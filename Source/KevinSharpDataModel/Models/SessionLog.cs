using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KevinSharp.DataModel
{
    public class SessionLog
    {
        [Column(Order = 1)]
        public int SessionLogId { get; set; }

        [Required]
        [Column(Order = 2)]
        public string SessionId { get; set; }

        [Required]
        [Column(Order = 3)]
        public DateTime SessionStartedUtc { get; set; }

        [Required]
        [Column(Order = 4)]
        public string SessionDuration { get; set; }
        
        [Column(Order = 5)]
        public string UserEmail { get; set; }

        [Required]
        [Column(Order = 6)]
        public string Events { get; set; }
    }
}
