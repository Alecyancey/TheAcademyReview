using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Program
{
    public class ProgramRatingDelete : RatingDelete
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
    }
}
