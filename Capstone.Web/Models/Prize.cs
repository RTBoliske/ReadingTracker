using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class Prize
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int UserType { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int Milestone { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int MaxNumPrizes { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool isActive { get; set; }


        public int FamilyID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "This field is required.")]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public string Title { get; set; }
    }
}