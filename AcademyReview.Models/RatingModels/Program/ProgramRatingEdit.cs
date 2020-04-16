using AcademyReview.Models.RatingModels.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Program
{
    public class ProgramRatingEdit : RatingEdit
    {
        public int ProgramId { get; set; }
    }
}
