using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class ReadingLogViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Book ID:")]
        public int BookID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Amount of time read:")]
        public int MinutesRead { get; set; }
        public bool Complete { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Reading book type:")]
        public string Type { get; set; }
    }
}