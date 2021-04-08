using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name="Role")]
        public string RoleName { get; set; }
    }
}