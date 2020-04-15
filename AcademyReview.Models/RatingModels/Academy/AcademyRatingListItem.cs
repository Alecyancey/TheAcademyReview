using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Academy
{
    public class AcademyRatingListItem : RatingListItem
    {
        public int AcademyId { get; set; }
        [Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    } 
}
