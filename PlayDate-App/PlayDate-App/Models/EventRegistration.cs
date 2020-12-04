using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class EventRegistration
    {
        [Key]
        public int EventRegistrationId { get; set; }

        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public Parent Parent { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

        public bool Accepted { get; set; }

        [Required]
        public string Role { get; set; }
        public bool ConfirmedAttendance { get; set; }


    }
}
