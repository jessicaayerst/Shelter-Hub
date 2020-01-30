using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models.ViewModels
{
    public class CreateClientWithImageViewModel
    {
        public Client Client { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }
    }
}
