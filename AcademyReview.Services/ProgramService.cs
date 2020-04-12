using AcademyReview.Data;
using AcademyReview.Models.ProgramModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool CreateProgramAsync(ProgramCreate model)
        {
            var ctx = new ApplicationDbContext();
            int academyId = ctx.Academies.FirstOrDefault(a => a.Name == model.AcademyName).AcademyId;
            Program entity = new Program
            {
                Name = model.Name,
                Type = model.Type,
                Prerequisite = model.Prerequisite,
                AcademyId = academyId,
                AcademyName = model.AcademyName
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
            return programList;
        }
        public ProgramDetail GetProgramByDetail(int id)
        {
            var model = new ProgramDetail();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.FirstOrDefault(i => i.AcademyId == id);
                //var programs = ctx.Programs.Where(p => p.AcademyId == id);
                model.ProgramId = entity.ProgramId;
                model.Name = entity.Name;
                model.Type = entity.Type;
                model.Prerequisite = entity.Prerequisite;
                model.AcademyId = entity.AcademyId;
                model.AcademyName = entity.AcademyName;

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
