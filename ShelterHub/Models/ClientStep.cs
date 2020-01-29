using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models
{
    public class ClientStep
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int StepId { get; set; }
        public Step Step { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }

    }
}
