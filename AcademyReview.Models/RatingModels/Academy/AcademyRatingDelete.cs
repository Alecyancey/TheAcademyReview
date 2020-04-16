using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Academy
{
    public class AcademyRatingDelete : RatingDelete
    {
        public int AcademyId { get; set; }
        public string Name { get; set; }
    }
}
