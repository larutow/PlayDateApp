using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LocationZip { get; set; }
        public string ImagePath { get; set; }
        public string EmailAddress { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int ThumbsUp { get; set; }


    }
}
