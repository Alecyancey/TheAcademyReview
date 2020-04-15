using AcademyReview.Data;
using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AcademyReview.Services
{
    public class ProgramService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;
        public ProgramService(string userId)
        {
            _context = new ApplicationDbContext();
            _userId = userId;
        }
        public bool CreateProgram(ProgramCreate model)
        {
            var ctx = new ApplicationDbContext();
            int academyId = ctx.Academies.FirstOrDefault(a => a.Name == model.AcademyName).AcademyId;
            string createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
            Program entity = new Program
            {
                Name = model.Name,
                Type = model.Type,
                Prerequisite = model.Prerequisite,
                AcademyId = academyId,
                AcademyName = model.AcademyName,
                OwnerId = _userId,
                CreatedBy = createdBy
            };
            _context.Programs.Add(entity);
            var changeCount = _context.SaveChanges();
            return changeCount == 1;
        }
        public async Task<List<ProgramListItem>> GetAllProgramsAsync()
        {
            var entityList = await _context.Programs.ToListAsync();

            var programList = entityList.Select(p => new ProgramListItem
            {
                ProgramId = p.ProgramId,
                Name = p.Name,
                AcademyName = p.AcademyName,
                AcademyId = p.AcademyId,
                Type = p.Type,
                Prerequisite = p.Prerequisite,
                AverageRating = p.AverageRating
            }).ToList();
            var sortedList = programList.OrderBy(a => a.AverageRating).ToList();
            sortedList.Reverse();
            return sortedList;
        }
        public ProgramDetail GetProgramByDetail(int id)
        {
            var model = new ProgramDetail();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.FirstOrDefault(i => i.ProgramId == id);
                var userName = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
                //var programs = ctx.Programs.Where(p => p.AcademyId == id);
                model.ProgramId = entity.ProgramId;
                model.Name = entity.Name;
                model.Type = entity.Type;
                model.Prerequisite = entity.Prerequisite;
                model.AcademyId = entity.AcademyId;
                model.AcademyName = entity.AcademyName;
                model.CreatedBy = entity.CreatedBy;

                //model.Programs = entity.Programs;
                //model.Instructors = entity.Instructors;
                //model.Ratings = entity.Ratings;
            }
            return model;
        }
        //GetProgramById
        public ProgramDetail GetProgramById(int id)
        {
            var model = new ProgramDetail();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.FirstOrDefault(a => a.ProgramId == id);
                model.Name = entity.Name;
                model.Type = entity.Type;
                model.Prerequisite = entity.Prerequisite;
                model.AcademyId = entity.AcademyId;
                model.AcademyName = entity.AcademyName;
            }
            return model;
        }
        //GetAcademiesForDropdown
        //public IEnumerable<SelectListItem> GetAcademiesForDropdown()
        //{
        //    var ctx = new ApplicationDbContext();
        //    List<SelectListItem> regions = new List<SelectListItem>();

        //    foreach (var academy in ctx.Academies)
        //    {
        //        new SelectListItem
        //        {
        //            Value = academy.AcademyId.ToString(),
        //            Text = academy.Name.ToString()
        //        };
        //    }

        //    return regions;
        //}
        //UpdateProgram
        public bool UpdateProgramByIdAsync(ProgramEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.Single(p => p.ProgramId == model.ProgramId);
                int academyId = ctx.Academies.Single(a => a.Name == entity.AcademyName).AcademyId;


                entity.ProgramId = model.ProgramId;
                entity.Name = model.Name;
                entity.Type = model.Type;
                entity.Prerequisite = model.Prerequisite;
                entity.AcademyId = academyId;
                entity.AcademyName = model.AcademyName;

                return ctx.SaveChanges() == 1;
            }
        }
        //DeleteProgram
        public bool DeleteProgramById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.Single(p => p.ProgramId == id);

                ctx.Programs.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
