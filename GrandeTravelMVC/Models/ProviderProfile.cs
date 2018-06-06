using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravelMVC.Models
{
    public class ProviderProfile
    {
        public int ProviderProfileId { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CompanyLogo { get; set; }
        public string UserId { get; set; }
    }
}
