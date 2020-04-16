using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Models.SearchModels
{
    public class SearchParameters
    {
        public SearchBy TypeId { get; set; }
        public string SearchInput { get; set; }
    }
    public enum SearchBy
    {
        Academy,
        Program,
        Instructor
    }
}
