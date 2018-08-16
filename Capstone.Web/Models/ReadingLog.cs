using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class ReadingLog
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int MinutesRead { get; set; }
        public bool Complete { get; set; }
        public string Type { get; set; }
    }
}