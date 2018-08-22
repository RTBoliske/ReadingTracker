using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class PrizeProgress
    {
        public int ID { get; set; }
        public int UserType { get; set; }
        public int Milestone { get; set; }
        public int MaxNumPrizes { get; set; }
        public bool isActive { get; set; }
        public int FamilyID { get; set; }
        public int UserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinutesRead { get; set; }
        public decimal PercentProgress { get; set; }
    }
}