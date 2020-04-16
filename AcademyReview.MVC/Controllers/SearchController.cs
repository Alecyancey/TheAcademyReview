using AcademyReview.Data;
using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.InstructorModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using AcademyReview.Models.SearchModels;
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
        public ActionResult Index(string id)
        {
            return View();
        }
        //public ActionResult Search()
        //{
        //    return View();
        //}
        //public ActionResult Search(SearchParameters model)
        //{
        //    if(model.TypeId == SearchBy.Academy)
        //    {
        //        var ctx = new ApplicationDbContext();
        //        var model = 
        //    }
        //}
        //public ActionResult Academies()
        //{
        //    return View();
        //}
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
        //public PartialViewResult Academies2()
        //{
        //    return PartialView();
        //}
        //public PartialViewResult Academies2(string id)
        //{
        //    var ctx = new ApplicationDbContext();
        //    var academies = from a in ctx.Academies
        //                    select a;

        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        academies = academies.Where(a => a.Name.Contains(id));
        //        //double averageRating = ctx.Academies.FirstOrDefault(a => a.Name.Contains(id)).AverageRating;
        //        var list = academies.Select(a => new AcademySearchItem
        //        {
        //            Id = a.AcademyId,
        //            Name = a.Name,
        //            City = a.City,
        //            State = a.State
        //        }).ToList();
        //        return PartialView(list);
        //    }
        //    var emptyList = new List<AcademySearchItem>();
        //    return PartialView(emptyList);
        //}
        public ActionResult Programs(string id)
        {
            var ctx = new ApplicationDbContext();
            var programs = from p in ctx.Programs
                            select p;

            if (!String.IsNullOrEmpty(id))
            {
                programs = programs.Where(a => a.Name.Contains(id));
                //double averageRating = ctx.Academies.FirstOrDefault(a => a.Name.Contains(id)).AverageRating;
                var list = programs.Select(a => new ProgramSearchItem
                {
                    Id = a.ProgramId,
                    Name = a.Name,
                    Type = a.Type,
                    AcademyName = a.AcademyName
                }).ToList();
                return View(list);
            }
            var emptyList = new List<ProgramSearchItem>();
            return View(emptyList);
        }
        public ActionResult Instructors(string id)
        {
            var ctx = new ApplicationDbContext();
            var instructors = from i in ctx.Instructors
                           select i;

            if (!String.IsNullOrEmpty(id))
            {
                instructors = instructors.Where(a => a.FullName.Contains(id));
                //double averageRating = ctx.Academies.FirstOrDefault(a => a.Name.Contains(id)).AverageRating;
                var list = instructors.Select(a => new InstructorSearchItem
                {
                    Id = a.InstructorId,
                    FullName = a.FullName,
                    AcademyName = a.AcademyName,
                    ProgramName = a.ProgramName
                }).ToList();
                return View(list);
            }
            var emptyList = new List<InstructorSearchItem>();
            return View(emptyList);
        }
    }
}