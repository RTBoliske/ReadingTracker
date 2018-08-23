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
            Success = 2,
            Incomplete = 3
        }

        
        public int PrizeId { get; set; }

        public int FamilyId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "User Type")]
        public int UserType { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Milestone")]
        public int Milestone { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int MaxNumPrizes { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool isActive { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Title")]
        public string Title { get; set; }


        public SuccessState AddSuccessState { get; set; }


        public List<Prize> PrizeList { get; set; }
    }
}