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
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult Create(AcademyCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = GetAcademyService();
                if (service.CreateAcademy(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult Rate(int id)
        {
            var service = GetAcademyService();
            ViewBag.Detail = service.GetAcademyById(id);

            var model = new AcademyRatingCreate { AcademyId = id };
            return View(model);
        }

        //HttpPost
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult Rate(AcademyRatingCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = new RatingService(User.Identity.GetUserId());
                if (service.CreateAcademyRating(model))
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
            @ViewBag.id = id;
            var model = service.GetAnAcademyRatings(id);
            //var modelList = new List<AcademyRatingListItem>();

            //ViewBag.Name = model.

            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteRating(int id)
        {
            var model = new AcademyRatingDelete { Id = id };

            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteRating(AcademyRatingDelete model)
        {
            if(ModelState.IsValid)
            {
                var service = new BrowsingService(User.Identity.GetUserId());
                if (service.DeleteRating(model.Id))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditRating(int id)
        {
            var model = new RatingEdit { Id = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditRating(RatingEdit model)
        {
            if (ModelState.IsValid)
            {
                var service = new RatingService(User.Identity.GetUserId());
                if (service.UpdateRating(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        //HttpGet 
        [HttpGet, Authorize (Roles = "Admin")]
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
        [HttpPost, Authorize (Roles = "Admin")]
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
        //HttpGet Details
        [HttpGet, AllowAnonymous]
        public ActionResult Details(int id)
        {
            var service = GetAcademyService();
            var model = service.GetAcademyByDetail(id);
            return View(model);
        }
        //HttpGet
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var service = GetAcademyService();

            var model = service.GetAcademyByDetail(id);

            return View(model);
        }
        [HttpPost, ActionName("Delete"), Authorize (Roles = "Admin")]
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