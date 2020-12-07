using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class ParentIndexViewModel
    {
        public Parent Parent { get; set; }
        public List<Kid> Kids { get; set; }
        public string NameSearch { get; set; }
        public int? AgeLow { get; set; }
        public int? AgeHigh { get; set; }
        public int? ZipSearch { get; set; }
        public bool ImmunizedSearch { get; set; }
        public bool WearsMaskSearch { get; set; }
        

    }
}
