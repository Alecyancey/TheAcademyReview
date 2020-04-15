using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.AcademyModels
{
    public class AcademySearchItem
    { //id, name, location, average rating
        public int Id { get; set; }
        [Display(Name = "Academy Name")]
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Average Rating")]
        public double AverageRating { get; set; }
    }
}
