using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Instructor
{
    public class InstructorRatingCreate : RatingCreate
    {
        public int InstructorId { get; set; }
    }
}
