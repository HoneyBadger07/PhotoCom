using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "The First name field is required."), Display(Name = "First name")]
        public string FIRST_NAME { get; set; } 
        [Required(ErrorMessage = "The Last name field is required."), Display(Name = "Last name")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage = "The Email field is required."), DataType(DataType.EmailAddress), Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required."), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Confirm Password field is required."), Display(Name = "Confirm Password"), DataType(DataType.Password), Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Remmberme me")]
        public Boolean Remmberme { get; set; }
    }
}
