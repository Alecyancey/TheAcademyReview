using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.InstructorModels
{
    public class InstructorSearchItem
    {
        public int Id { get; set; }
        [Display(Name = "Instructor Name")]
        public string FullName { get; set; }
        [Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
    }
}
