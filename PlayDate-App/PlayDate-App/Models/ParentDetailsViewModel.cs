using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class ParentDetailsViewModel
    {
        public Parent Parent { get; set; }
        public List<Kid> Kids { get; set; }
    }
}
