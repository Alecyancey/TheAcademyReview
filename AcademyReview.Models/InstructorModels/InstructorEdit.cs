using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.InstructorModels
{
    public class InstructorEdit
    {
        [Required]
        public int InstructorId { get; set; }
        [Required, Display(Name = "Instructor Name")]
        public string FullName { get; set; }
        [Required]
        public int AcademyId { get; set; }
        [Required, Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
        [Required]
        public int ProgramId { get; set; }
        [Required, Display(Name = "Program Name")]
        public string ProgramName { get; set; }
    }
}
