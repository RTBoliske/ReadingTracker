using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class Book
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int FamilyID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Type { get; set; }
    }
}