using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Data
{


    ////////////////////      Fix naming and add User info to tables      ///////////////////////////


    public class Academy
    {
        [Key]
        public int AcademyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        //
        public string OwnerId { get; set; }
        public string CreatedBy { get; set; }
        //
        public double AverageRating
        {
            get
            {
                if (Ratings !=null && Ratings.Count != 0)
                    return (double)Ratings.Sum(rating => rating.Score) / Ratings.Count;

                return 0;
            }
        }
        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<AcademyRating> Ratings { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
