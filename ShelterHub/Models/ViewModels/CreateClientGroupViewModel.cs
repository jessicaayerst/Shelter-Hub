﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models.ViewModels
{
    public class CreateClientGroupViewModel
    {
        public ClientGroup ClientGroup { get; set; }
        public List<SelectListItem> ClientNameOptions { get; set; }
     
    }
}
