using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Web.Models
{
    public class AccountRegisterModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string LoginEmail { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        [Display(Name = "CreditCard")]
        public string CreditCard { get; set; }

        [Display(Name = "ExpDate")]
        public string ExpDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}