using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravelMVC.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public string PackageName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
