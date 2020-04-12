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
        [Required]
        public string FullName { get; set; }
        [Required]
        public int AcademyId { get; set; }
        [Required]
        public string AcademyName { get; set; }
        [Required]
        public int ProgramId { get; set; }
        [Required]
        public string ProgramName { get; set; }
    }
}
