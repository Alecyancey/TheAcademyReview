using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Academy
{
    public class AcademyRatingListItem : RatingListItem
    {

        public int AcademyId { get; set; }
        public string AcademyName { get; set; }
        public string AcademyRating { get; set; }

    } 
}
