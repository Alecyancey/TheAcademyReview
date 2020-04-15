using AcademyReview.Data;
using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.RatingModels.Academy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademyReview.MVC.Controllers
{
    public class SearchController : Controller
    {
        // GET: Rating
        //public ActionResult Index()
        //{

        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Academies(string id)
        {
            var ctx = new ApplicationDbContext();
            var academies = from a in ctx.Academies
                            select a;

            if (!String.IsNullOrEmpty(id))
            {
                academies = academies.Where(a => a.Name.Contains(id));
                //double averageRating = ctx.Academies.FirstOrDefault(a => a.Name.Contains(id)).AverageRating;
                var list = academies.Select(a => new AcademySearchItem
                {
                    Id = a.AcademyId,
                    Name = a.Name,
                    City = a.City,
                    State = a.State
                }).ToList();
            return View(list);
            }
            var emptyList = new List<AcademySearchItem>();
            return View(emptyList);
        }
    }
}