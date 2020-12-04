using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime TimeAndDate { get; set; }
        public bool ConfirmedEvent { get; set; }
        public bool IsPrivate { get; set; }
        public int Capacity { get; set; }


    }
}
