using System.ComponentModel.DataAnnotations;

namespace CrbAuth.Web.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}