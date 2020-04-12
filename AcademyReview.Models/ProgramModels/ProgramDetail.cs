using AcademyReview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class ProgramDetail
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Prerequisite { get; set; }
        public int AcademyId { get; set; }
        public virtual Academy Academy { get; set; }
        public string AcademyName { get; set; }
        public virtual ICollection<ProgramRating> Ratings { get; set; }
    }
}
