﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class ReadingLog
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int FamilyID { get; set; }
        public int MinutesRead { get; set; }
        public bool Complete { get; set; }
        public DateTime Date { get; set; }
    }
}