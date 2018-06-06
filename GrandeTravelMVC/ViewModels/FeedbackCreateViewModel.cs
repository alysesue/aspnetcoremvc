using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GrandeTravelMVC.ViewModels
{
    public class FeedbackCreateViewModel
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(256)]
        public string FDescription { get; set; }

        public int PackageId { get; set; }

        public int UserId { get; set; }
    }
}
