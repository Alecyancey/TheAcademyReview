using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Program
{
    public class ProgramRatingListItem : RatingListItem
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
    }
}
