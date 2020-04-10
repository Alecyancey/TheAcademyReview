using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Data
{
    public abstract class Rating
    {
        [Key]
        public int RatingId { get; set; }
        [Required, Range(1, 5)]
        public int Score { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Description { get; set; }
    }
    public class AcademyRating : Rating 
    {
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
        public virtual Academy Academy { get; set; }

    }
    public class ProgramRating : Rating
    {
        [ForeignKey(nameof(Program))]
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
    }
    public class InstructorRating : Rating
    {
        [ForeignKey(nameof(Instructor))]
        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
