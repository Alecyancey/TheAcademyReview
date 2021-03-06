﻿using AcademyReview.Models.InstructorModels;
using AcademyReview.Models.RatingModels.Academy;
using AcademyReview.Models.RatingModels.Instructor;
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
    public class InstructorController : Controller
    {
        // GET: Instructor
        public ActionResult Index()
        {
            var service = GetInstructorService();
            return View(service.GetAllInstructors());
        }
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Create(InstructorCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = GetInstructorService();
                if (service.CreateInstructor(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        //HttpGet Rate(int id)
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Rate(int id)
        {
            var service = GetInstructorService();
            ViewBag.Detail = service.GetInstructorById(id);
            var entity = service.GetInstructorById(id);
            var model = new InstructorRatingCreate { InstructorId = entity.InstructorId };
            return View(model);
        }
        //HttpPost Rate(ProgramRatingCreate model)
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult Rate(InstructorRatingCreate model)
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
        public ActionResult Ratings(int id)
        {
            var service = new RatingService(User.Identity.GetUserId());
            var model = service.GetAnInstructorRatings(id);

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
        //HttpGet Edit
        [HttpGet, Authorize (Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var service = GetInstructorService();
            var model = service.GetInstructorById(id);
            //int academyId = model.AcademyId;
            //int programId = model.ProgramId;
            //string academyName = service.GetAcademyNameById(academyId);
            //string programName = service.GetProgramNameById(programId);
            var newModel = new InstructorEdit();
            {
                newModel.InstructorId = model.InstructorId;
                newModel.FullName = model.FullName;
                newModel.ProgramId = model.ProgramId;
                newModel.ProgramName = model.ProgramName;
                newModel.AcademyId = model.AcademyId;
                newModel.AcademyName = model.AcademyName;
            };
            return View(model);
        }
        //HttpPost Edit
        [HttpPost, Authorize (Roles = "Admin")]
        public ActionResult Edit(InstructorEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetInstructorService();
            if (service.UpdateInstructor(model))
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
            var service = GetInstructorService();
            var model = service.GetInstructorByDetail(id);

            return View(model);
        }
        //HttpGet Delete
        [HttpGet, Authorize (Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var service = GetInstructorService();

            var model = service.GetInstructorById(id);

            return View(model);
        }
        //HttpPost Delete
        [HttpPost, ActionName("Delete"), Authorize (Roles = "Admin")]
        public ActionResult DeleteProgram(int id)
        {
            var service = GetInstructorService();
            if (service.DeleteInstructorById(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
        private InstructorService GetInstructorService()
        {

            var userId = User.Identity.GetUserId();
            var service = new InstructorService(userId);
            return service;
        }
    }
}