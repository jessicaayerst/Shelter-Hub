using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models
{
    public class Step
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string StepName { get; set; }
        public bool IsArchived { get; set; }
        public int StepTypeId { get; set; }
        public StepType StepType { get; set; }
        public List<ClientStep> ClientSteps { get; set; } = new List<ClientStep>();

    }
}
