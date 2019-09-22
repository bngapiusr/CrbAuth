using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrbAuth.Web.ViewModels
{
    public class AddUserViewModel
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string   UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ConfirmedEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Password can not be less then 8 characters.")]
        public string Password { get; set; }

    }
}
