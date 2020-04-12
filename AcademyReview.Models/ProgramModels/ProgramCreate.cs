using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class ProgramCreate
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Prerequisite { get; set; }
        public int AcademyId { get; set; }
        public string AcademyName { get; set; }
    }
}
