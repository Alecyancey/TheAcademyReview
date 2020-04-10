using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.RatingModels
{
    public class RatingCreate
    {
        [Required, Range(1, 5)]
        public int Score { get; set; }
        public string Description { get; set; }
    }
}
