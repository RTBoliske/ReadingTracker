using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class AddBookViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Author { get; set; }

        public List<Book> BookList { get; set; }

        public enum SuccessState
        {
            None = 0,
            Failed = 1,
            Success = 2
        }

        public SuccessState AddSuccessState { get; set; }
    }
}