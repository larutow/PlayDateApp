using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class Kid
    {
        [Key]
        public int KidId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public int Age { get; set; }
        public bool Immunized { get; set; }
        public bool WearsMask { get; set; }
        public bool SpecialNeeds { get; set; }
        public string Notes { get; set; }
        
        [ForeignKey("Parent")]
        public int ParentId { get; set; }

    }
}
