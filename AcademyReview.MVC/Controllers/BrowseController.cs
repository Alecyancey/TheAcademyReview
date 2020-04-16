using AcademyReview.Data;
using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.InstructorModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using AcademyReview.Models.RatingModels.Instructor;
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
    public class BrowseController : Controller
    {
        // GET: Browse
        public async Task<ActionResult> Index() //Academies//
        {
            var service = GetBrowsingService();
            return View(await service.GetBrowsingAcademies());
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult CreateAcademy()
        {
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult CreateAcademy(AcademyCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = GetBrowsingService();
                if (service.CreateAcademy(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult CreateProgram()
        {
            return View();
        }
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult CreateProgram(ProgramCreate model)
        {
            var ctx = new ApplicationDbContext();
            if (ctx.Academies.FirstOrDefault(a => a.Name == model.AcademyName) == null)
            {
                ModelState.AddModelError("AcademyName", "Please create the academy first or enter an academy that already exists");
                return View(model);
            }
            var service = GetBrowsingService();
            if (service.CreateProgram(model))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult CreateInstructor()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult CreateInstructor(InstructorCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = GetBrowsingService();
                if (service.CreateInstructor(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            //ModelState.AddModelError("AcademyName", "Please create the academy first or enter an academy that already exists");

            return View(model);
        }
        public ActionResult Programs(int id)
        {
            var service = GetBrowsingService();
            var model = service.GetAcademysPrograms(id);

            return View(model);
        }
        public ActionResult Instructors(int id)
        {
            var service = GetBrowsingService();
            var model = service.GetProgramsInstructors(id);

            return View(model);
        }
        [HttpGet, AllowAnonymous]
        public ActionResult AcademyDetails(int id)
        {
            var service = GetBrowsingService();
            var model = service.GetAcademyByDetail(id);
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult RateAcademy(int id)
        {
            var service = GetBrowsingService();
            ViewBag.Detail = service.GetAcademyById(id);

            var model = new AcademyRatingCreate { AcademyId = id };
            return View(model);
        }

        //HttpPost
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult RateAcademy(AcademyRatingCreate model)
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
        public ActionResult AcademyRatings(int id)
        {
            var service = new RatingService(User.Identity.GetUserId());
            @ViewBag.id = id;
            var model = service.GetAnAcademyRatings(id);
            //var modelList = new List<AcademyRatingListItem>();

            //ViewBag.Name = model.

            return View(model);
        }
        [HttpGet]
        public ActionResult EditAcademyRating(int id)
        {
            var model = new AcademyRatingEdit { Id = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditAcademyRating(AcademyRatingEdit model)
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
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult EditAcademy(int id)
        {
            var service = GetBrowsingService();

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
        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult EditAcademy(AcademyEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetBrowsingService();
            if (service.UpdateAcademy(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Academy couldn't be updated.");
            return View(model);
        }
        public ActionResult DeleteAcademyRating(int id)
        {
            var model = new AcademyRatingDelete { Id = id };

            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteAcademyRating(AcademyRatingDelete model)
        {
            if (ModelState.IsValid)
            {
                var service = new BrowsingService(User.Identity.GetUserId());
                if (service.DeleteRating(model.Id))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult DeleteAcademyView(int id)
        {
            var service = GetBrowsingService();

            var model = service.GetAcademyByDetail(id);

            return View(model);
        }
        [HttpDelete, ActionName("Delete"), Authorize(Roles = "Admin")]
        public ActionResult DeleteAcademy(int id)
        {
            var service = GetBrowsingService();
            if (service.DeleteAcademyById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult ProgramDetails(int id)
        {
            var service = GetBrowsingService();
            var model = service.GetProgramByDetail(id);
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult RateProgram(int id)
        {
            var service = GetBrowsingService();
            ViewBag.Detail = service.GetProgramById(id);

            var model = new ProgramRatingCreate { ProgramId = id };
            return View(model);
        }

        //HttpPost
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult RateProgram(ProgramRatingCreate model)
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
        public ActionResult ProgramRatings(int id)
        {
            var service = new RatingService(User.Identity.GetUserId());
            @ViewBag.id = id;
            var model = service.GetAnProgramRatings(id);
            //var modelList = new List<AcademyRatingListItem>();

            //ViewBag.Name = model.

            return View(model);
        }
        [HttpGet]
        public ActionResult EditProgramRating(int id)
        {
            var model = new ProgramRatingEdit { Id = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProgramRating(ProgramRatingEdit model)
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
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult EditProgram(int id)
        {
            var service = GetBrowsingService();

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

        //HttpPost
        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult EditProgram(ProgramEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetBrowsingService();
            if (service.UpdateProgram(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Program couldn't be updated.");
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult DeleteProgramView(int id)
        {
            var service = GetBrowsingService();

            var model = service.GetProgramByDetail(id);

            return View(model);
        }
        [HttpDelete, ActionName("Delete"), Authorize(Roles = "Admin")]
        public ActionResult DeleteProgram(int id)
        {
            var service = GetBrowsingService();
            if (service.DeleteProgramById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult DeleteProgramRating(int id)
        {
            var service = GetBrowsingService();

            var model = service.GetProgramById(id);

            return View(model);
        }
        //HttpPost Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteRating(int id)
        {
            var service = GetBrowsingService();
            if (service.DeleteProgramById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult InstructorDetails(int id)
        {
            var service = GetBrowsingService();
            var model = service.GetInstructorByDetail(id);
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin, User")]
        public ActionResult RateInstructor(int id)
        {
            var service = GetBrowsingService();
            ViewBag.Detail = service.GetInstructorById(id);

            var model = new InstructorRatingCreate { InstructorId = id };
            return View(model);
        }

        //HttpPost
        [HttpPost, Authorize(Roles = "Admin, User")]
        public ActionResult RateInstructor(InstructorRatingCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = new RatingService(User.Identity.GetUserId());
                if (service.CreateInstructorRating(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet, AllowAnonymous]
        public ActionResult InstructorRatings(int id)
        {
            var service = new RatingService(User.Identity.GetUserId());
            @ViewBag.id = id;
            var model = service.GetAnInstructorRatings(id);
            //var modelList = new List<AcademyRatingListItem>();

            //ViewBag.Name = model.

            return View(model);
        }
        [HttpGet]
        public ActionResult EditInstructorRating(int id)
        {
            var model = new InstructorRatingEdit { Id = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditInstructorRating(InstructorRatingEdit model)
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
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult EditInstructor(int id)
        {
            var service = GetBrowsingService();

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

        //HttpPost
        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult EditIntructor(InstructorEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetBrowsingService();
            if (service.UpdateInstructor(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Instructor couldn't be updated.");
            return View(model);
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult DeleteInstructorView(int id)
        {
            var service = GetBrowsingService();

            var model = service.GetInstructorByDetail(id);

            return View(model);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult DeleteInstructor(int id)
        {
            var service = GetBrowsingService();
            if (service.DeleteInstructorById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult DeleteInstructorRatingView(int id)
        {
            var service = GetBrowsingService();

            var model = service.GetInstructorByDetail(id);

            return View(model);
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult DeleteInstructorRating(int id)
        {
            var service = GetBrowsingService();
            if (service.DeleteInstructorById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        ////HttpPost Delete
        //[HttpPost]
        //public ActionResult DeleteInstructorRating(int id)
        //{
        //    var service = GetBrowsingService();

        //    var model = service.GetInstructorByDetail(id);

        //    return View(model);
        //}
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private BrowsingService GetBrowsingService()
        {
            var userId = User.Identity.GetUserId();
            var service = new BrowsingService(userId);
            return service;
        }
    }
}