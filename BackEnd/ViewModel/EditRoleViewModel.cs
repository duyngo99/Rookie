using System.Collections.Generic;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.ViewModel
{
    public class EditRoleViewModel : IdentityRole
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