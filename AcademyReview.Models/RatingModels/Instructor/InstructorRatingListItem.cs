using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Instructor
{
    public class InstructorRatingListItem : RatingListItem
    {
        public int InstructorId { get; set; }
        public string FullName { get; set; }
    }
}
