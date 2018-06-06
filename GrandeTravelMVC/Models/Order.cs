using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravelMVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string PackageName { get; set; }
        public int PackageId { get; set; }
        public string UserId { get; set; }
    }
}
