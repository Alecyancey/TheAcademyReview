using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Data
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        [Required]
        public string FullName { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
        public virtual Academy Academy { get; set; }
        [ForeignKey(nameof(Program))]
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public double AverageRating
        {
            get
            {
                if (Ratings != null && Ratings.Count != 0)
                    return (double)Ratings.Sum(rating => rating.Score) / Ratings.Count;

                return 0;
            }
        }
        public virtual ICollection<InstructorRating> Ratings { get; set; }
    }
}
