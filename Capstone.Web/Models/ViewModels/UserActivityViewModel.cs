﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class UserActivityViewModel
    {
        public int ReadingLogID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int FamilyID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public string ISBN { get; set; }
        public int MinutesRead { get; set; }
        public bool Complete { get; set; }
        public string Type { get; set; }
        public bool BookActive { get; set; }
        public List<User> UserList { get; set; }
    }
}