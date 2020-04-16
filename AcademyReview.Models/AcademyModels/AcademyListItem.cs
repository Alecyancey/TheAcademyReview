using AcademyReview.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.AcademyModels
{
    public class AcademyListItem
    {
        [Display(Name = "")]
        public int AcademyId { get; set; }
        [Display(Name = "Academy Name")]
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name="Average Rating")]
        public double AverageRating { get; set; }
        [Display(Name = "Program Count")]
        public int ProgramCount { get; set; }
    }
}
