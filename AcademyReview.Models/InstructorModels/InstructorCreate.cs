using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.InstructorModels
{
    public class InstructorCreate
    {
        [Required, Display(Name = "Instructor Name")]
        public string FullName { get; set; }
        //public int AcademyId { get; set; }
        [Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
        //AcademyName?
        [Required]
        //public int ProgramId { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
        //ProgramName?
    }
}
