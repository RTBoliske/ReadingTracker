using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class PrizeViewModel
    {
        public enum SuccessState
        {
            None = 0,
            Failed = 1,
            Success = 2
        }

        public int UserType { get; set; }
        public int Milestone { get; set; }
        public int MaxNumPrizes { get; set; }
        public bool isActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SuccessState AddSuccessState { get; set; }
        public List<Prize> PrizeList { get; set; }
    }
}