using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class RegisterViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //[Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //[Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //[Display(Name = "Family Name:")]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //[Display(Name = "Username:")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //[Display(Name = "Password:")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        //[Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }
        public int RoleID { get; set; } = 2;
        public int FamilyID { get; set; }
    }
}