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
        public async Task<ActionResult> Index()
        {
            var service = GetProgramService();
            return View(await service.GetAllProgramsAsync());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProgramCreate model)
        {
            var service = GetProgramService();
            if(service.CreateProgramAsync(model))
            {
            return RedirectToAction(nameof(Index));
            }
            else
            {
            return View(model);
            }
        }
        //HttpGet Rate(int id)
        [HttpGet, Authorize]
        public ActionResult Rate(int id)
        {
            var service = GetProgramService();
            ViewBag.Detail = service.GetProgramById(id);

            var model = new ProgramRatingCreate { ProgramId = id };
            return View(model);
        }
        //HttpPost Rate(ProgramRatingCreate model)
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<ActionResult> Rate(ProgramRatingCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = new RatingService(User.Identity.GetUserId());
                if (await service.CreateProgramRatingAsync(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        //HttpGet Edit
        [HttpGet, Authorize]
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
        [HttpPost, Authorize, ValidateAntiForgeryToken]
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
        //HttpGet Delete
        [HttpGet]
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