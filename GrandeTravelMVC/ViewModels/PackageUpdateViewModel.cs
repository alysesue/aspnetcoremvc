using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GrandeTravelMVC.ViewModels
{
    public class PackageUpdateViewModel
    {
        public int PackageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PackageName { get; set; }

        public string LocationName { get; set; }

        public SelectList LocationList { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        public bool IsAvailable { get; set; }


        public int LocationId { get; set; }

        public string UserId { get; set; }
    }
}
