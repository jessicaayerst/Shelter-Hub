using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShelterHub.Models
{
    public class ApplicationUser : IdentityUser
        
    {
        [Required]
        [Display(Name = "Shelter Name")]
        public string ShelterName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

    }
}
