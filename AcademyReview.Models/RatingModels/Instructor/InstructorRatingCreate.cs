using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Instructor
{
    public class InstructorRatingCreate : RatingCreate
    {
        public int InstructorId { get; set; }
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }
        [Display(Name = "Instructor Rating")]
        public int InstructorRating { get; set; }

    }
}
