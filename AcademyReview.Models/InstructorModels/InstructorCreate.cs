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
        [Required]
        public string FullName { get; set; }
        [Required]
        public int AcademyId { get; set; }
        //AcademyName?
        [Required]
        public int ProgramId { get; set; }
        //ProgramName?
    }
}
