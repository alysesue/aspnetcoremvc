using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;

namespace GrandeTravelMVC.ViewModels
{
    public class AccountUpdateProvProfileViewModel
    {
        //public string UserName { get; set; }
        public string CompanyName { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Must be valid url")]
        public string Website { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Must be valid phone number")]
        public string Phone { get; set; }

        public string Address { get; set; }
        public string CompanyLogo { get; set; }
    }
}
