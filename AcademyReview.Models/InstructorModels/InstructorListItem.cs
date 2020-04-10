using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.InstructorModels
{
    public class InstructorListItem
    {
        public string FullName { get; set; }
        [Display(Name = "Average Rating")]
        public double AverageRating { get; set; }

    }
}
