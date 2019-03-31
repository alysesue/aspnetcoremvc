using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;
using System.ComponentModel.DataAnnotations;


namespace GrandeTravelMVC.ViewModels
{
    public class PackageSearchViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Package> Packages { get; set; }

        public string SearchString { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
