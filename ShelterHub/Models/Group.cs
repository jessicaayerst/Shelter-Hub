using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterHub.Models
{
    public class Group
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string GroupName { get; set; }
        public int MaxAttendees { get; set; }
        public string Time { get; set; }
        public string DayOfWeek { get; set; }
        public string Place { get; set; }
        public List<ClientGroup> ClientGroups { get; set; } = new List<ClientGroup>();
    }
}
