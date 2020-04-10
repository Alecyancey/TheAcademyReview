using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Data
{
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

    }
}
