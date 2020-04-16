using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class ProgramCreate
    {
        [Required, Display(Name = "Program Name")]
        public string Name { get; set; }
        [Display(Name = "Program Type")]
        public string Type { get; set; }
        public string Prerequisite { get; set; }
        public int AcademyId { get; set; }
        [Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
    }
}
