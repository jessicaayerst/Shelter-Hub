using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models
{
    public class Client
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + LastName; } }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool OkToText { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyName { get; set; }
        public string AssignedStaff { get; set; }
        public int RoomNumber { get; set; }
        public bool IntakeComplete { get; set; }
        public List<ClientStep> ClientSteps { get; set; } = new List<ClientStep>();
        public List<ClientGroup> ClientGroups { get; set; } = new List<ClientGroup>();
        public DateTime IntakeDate { get; set; }
        public DateTime? DepartDate { get; set; }

    }
}
