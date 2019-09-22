using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrbAuth.Web.ViewModels
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Confirmed Email")]
        public string ConfirmedEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 5)]
        public string Password { get; set; }

    }
}
