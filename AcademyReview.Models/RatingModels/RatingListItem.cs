﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.ProgramModels
{
    public class RatingListItem
    {
        public int RatingId { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public bool IsUserOwned { get; set; }
        public string UserId { get; set; }
    }
}
