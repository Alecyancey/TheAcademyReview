using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Data
{
    public class Program
    {
        [Key]
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Prerequisite { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
        public virtual Academy Academy { get; set; }
        public double AverageRating
        {
            get
            {
                if (Ratings != null && Ratings.Count != 0)
                    return (double)Ratings.Sum(rating => rating.Score) / Ratings.Count;

                return 0;
            }
        }
        public virtual ICollection<ProgramRating> Ratings { get; set; }
    }
}
