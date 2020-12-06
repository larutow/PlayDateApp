﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class ParentIndexViewModel
    {
        public Parent Parent { get; set; }
        public string NameSearch { get; set; }
        public int AgeLow { get; set; }
        public int AgeHigh { get; set; }
        public int ZipSearch { get; set; }

    }
}
