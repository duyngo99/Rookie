using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Models
{
    public class Role : IdentityRole
    {
        
        public string ID { get; set; }

        public string RoleName { get; set; }
        
    }
}