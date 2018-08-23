using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class AddFamilyMemberViewModel
    {
        public enum SuccessState
        {
            None = 0,
            Failed = 1,
            Success = 2
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "This field is required")]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        //[Display(Name = "Confirm Password:")]
        //public string ConfirmPassword { get; set; }

        public int RoleID { get; set; }

        public int FamilyID { get; set; }

        public List<User> FamilyMembersList { get; set; }

        public SuccessState AddSuccessState {get; set;}
    }
}