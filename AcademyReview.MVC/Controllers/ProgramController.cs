using AcademyReview.Data;
using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Program;
using AcademyReview.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademyReview.MVC.Controllers
{
    public class ProgramController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var service = GetProgramService();
            return View(await service.GetAllProgramsAsync());
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult Create(ProgramCreate model)
        {
            var ctx = new ApplicationDbContext();
            if (ctx.Academies.FirstOrDefault(a=>a.Name == model.AcademyName)==null)
            {
                ModelState.AddModelError("AcademyName", "Please create the academy first or enter an academy that already exists");
                return View(model);
            }
            var service = GetProgramService();
            if (service.CreateProgram(model))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        //HttpGet Rate(int id)
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult Rate(int id)
        {
            var service = GetProgramService();
            ViewBag.Detail = service.GetProgramById(id);

            var model = new ProgramRatingCreate { ProgramId = id };
            return View(model);
        }
        //HttpPost Rate(ProgramRatingCreate model)
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult Rate(ProgramRatingCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = new RatingService(User.Identity.GetUserId());
                if (service.CreateProgramRating(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet, AllowAnonymous]
        public ActionResult Ratings(int id)
        {
            var service = new RatingService(User.Identity.GetUserId());
            var model = service.GetAnProgramRatings(id);

            return View(model);
        }
        //HttpGet Edit
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var service = GetProgramService();

            var detail = service.GetProgramById(id);
            var model = new ProgramEdit
            {
                ProgramId = detail.ProgramId,
                Name = detail.Name,
                Type = detail.Type,
                Prerequisite = detail.Prerequisite,
                AcademyId = detail.AcademyId,
                AcademyName = detail.AcademyName
            };
            return View(model);
        }
        //HttpPost Edit
        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Edit(ProgramEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetProgramService();
            if (service.UpdateProgramByIdAsync(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Academy couldn't be updated.");
            return View(model);
        }
        [HttpGet, AllowAnonymous]
        public ActionResult Details(int id)
        {
            var service = GetProgramService();
            var model = service.GetProgramByDetail(id);

            return View(model);
        }
        //HttpGet Delete
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var service = GetProgramService();

            var model = service.GetProgramById(id);

            return View(model);
        }
        //HttpPost Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProgram(int id)
        {
            var service = GetProgramService();
            if (service.DeleteProgramById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        private ProgramService GetProgramService()
        {

            var userId = User.Identity.GetUserId();
            var service = new ProgramService(userId);
            return service;
        }
    }
}