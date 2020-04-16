using AcademyReview.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class ProgramDetail
    {
        public int ProgramId { get; set; }
        [Display(Name = "Program Name")]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Prerequisite { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        public int AcademyId { get; set; }
        public virtual Academy Academy { get; set; }
        [Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
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
        public virtual ICollection<ProgramRating> Ratings { get; set; }
    }
}
