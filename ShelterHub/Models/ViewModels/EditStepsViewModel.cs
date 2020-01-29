using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models.ViewModels
{
    public class EditStepsViewModel
    {
        public Step Step { get; set; }
        public List<SelectListItem> StepTypeOptions { get; set; }
    }
}
