using AcademyReview.Models.InstructorModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.AcademyModels
{
    public class AcademyDetail
    {
        public int AcademyId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public List<ProgramListItem> Programs { get; set; }
        public List<AcademyRatingListItem> Ratings { get; set; }
        public List<InstructorListItem> Instructors { get; set; }
    }
}
