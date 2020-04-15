using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Instructor
{
    public class InstructorRatingListItem : RatingListItem
    {
        public int InstructorId { get; set; }
        [Display(Name = "Instructor Name")]
        public string FullName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

    }
}
