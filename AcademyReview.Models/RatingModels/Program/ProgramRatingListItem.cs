using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels.Program
{
    public class ProgramRatingListItem : RatingListItem
    {
        public int ProgramId { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

    }
}
