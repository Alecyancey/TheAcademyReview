using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.RatingModels.Academy;
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
    public class AcademyController : Controller
    {
        // GET: Academy
        public async Task<ActionResult> Index()
        {
            var service = GetAcademyService();
            return View(await service.GetAllAcademiesAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcademyCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = GetAcademyService();
                if (await service.CreateAcademyAsync(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet, Authorize]
        public ActionResult Rate(int id)
        {
            var service = GetAcademyService();
            ViewBag.Detail = service.GetAcademyById(id);

            var model = new AcademyRatingCreate { AcademyId = id };
            return View(model);
        }

        //HttpPost
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<ActionResult> Rate(AcademyRatingCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = new RatingService(User.Identity.GetUserId());
                if (await service.CreateAcademyRatingAsync(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        //HttpGet 
        [HttpGet, Authorize]
        public ActionResult Edit(int id)
        {
            var service = GetAcademyService();

            var detail = service.GetAcademyById(id);
            var model = new AcademyEdit
            {
                AcademyId = id,
                Name = detail.Name,
                City = detail.City,
                State = detail.State
            };
            return View(model);
        }

        //HttpPost
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Edit(AcademyEdit model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetAcademyService();
            if (service.UpdateAcademyByIdAsync(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Academy couldn't be updated.");
                return View(model);
        }

        //HttpGet
        public ActionResult Delete(int id)
        {
            var service = GetAcademyService();

            var model = service.GetAcademyById(id);

            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAcademy(int id)
        {
            var service = GetAcademyService();
            if (service.DeleteAcademyById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        private AcademyService GetAcademyService()
        {
            var userId = User.Identity.GetUserId();
            var service = new AcademyService(userId);
            return service;
        }
    }
}