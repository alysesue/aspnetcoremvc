using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;

namespace GrandeTravelMVC.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Package> Packages { get; set; }
    }
}
