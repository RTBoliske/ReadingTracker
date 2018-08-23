using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class LoginViewModel
    {


        [Required(ErrorMessage = "This field is required.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }
    }
}