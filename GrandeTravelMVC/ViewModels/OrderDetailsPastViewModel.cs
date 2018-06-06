using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;

namespace GrandeTravelMVC.ViewModels
{
    public class OrderDetailsPastViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public int Count { get; set; }
    }
}
