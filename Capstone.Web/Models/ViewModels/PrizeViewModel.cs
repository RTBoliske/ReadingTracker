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

        
        public int PrizeId { get; set; }

        public int FamilyId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int UserType { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int Milestone { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int MaxNumPrizes { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool isActive { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "This field is required.")]
        public DateTime EndDate { get; set; } = DateTime.Today;


        public SuccessState AddSuccessState { get; set; }


        public List<Prize> PrizeList { get; set; }
    }
}