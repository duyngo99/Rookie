using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
            Claims = new List<string>();
        }

        public string ID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}