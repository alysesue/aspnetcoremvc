using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GrandeTravelMVC.ViewModels
{
    public class OrderCreateViewModel
    {
        public int PackageId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        public int Quantity { get; set; }

        public double TotalPrice { get; set; }
    }
}
