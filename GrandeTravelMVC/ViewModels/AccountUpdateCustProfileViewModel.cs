using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;

namespace GrandeTravelMVC.ViewModels
{
    public class AccountUpdateCustProfileViewModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage ="User name must be an email")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Must be valid phone number")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
    }
}
