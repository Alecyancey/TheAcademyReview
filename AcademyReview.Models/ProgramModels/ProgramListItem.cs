using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class ProgramListItem
    {
        [Display(Name ="")]
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Prerequisite { get; set; }
        public double AverageRating { get; set; }

        //public bool HasHeightRequirement
        //{
        //    get
        //    {
        //        return MinRiderHeight.HasValue;
        //    }
        //}
        //[Display(Name = "Minimum Required Height")]
        //public int? MinRiderHeight { get; set; }
    }
}
