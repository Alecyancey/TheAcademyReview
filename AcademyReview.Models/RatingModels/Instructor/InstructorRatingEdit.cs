using AcademyReview.Models.RatingModels.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Instructor
{
    public class InstructorRatingEdit : RatingEdit
    {
        public int InstructorId { get; set; }
    }
}
