using AcademyReview.Models.InstructorModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.AcademyModels
{
    public class AcademyDetail
    {
        public int AcademyId { get; set; }
        [Display (Name = "Academy Name")]
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Average Rating")]
        public double AverageRating
        {
            get
            {
                if (Ratings != null && Ratings.Count != 0)
                    return (double)Ratings.Sum(rating => rating.Score) / Ratings.Count;

                return 0;
            }
        }
        public List<ProgramListItem> Programs { get; set; }
        public List<AcademyRatingListItem> Ratings { get; set; }
        public List<InstructorListItem> Instructors { get; set; }
    }
}
