using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class ProgramSearchItem
    {
        public int Id { get; set; }
        [Display(Name = "Program Name")]
        public string Name { get; set; }
        public string Type { get; set; }
        [Display(Name = "Academy Name")]
        public string AcademyName { get; set; }
    }
}
