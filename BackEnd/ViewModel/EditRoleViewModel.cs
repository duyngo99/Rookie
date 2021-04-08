using System.Collections.Generic;
using BackEnd.Models;

namespace BackEnd.ViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string ID { get; set;}

        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}