using AcademyReview.Data;
using AcademyReview.Models.InstructorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Services
{
    public class InstructorService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;
        public InstructorService(string userId)
        {
            _context = new ApplicationDbContext();
            _userId = userId;
        }
        public bool CreateInstructor(InstructorCreate model)
        {
            var ctx = new ApplicationDbContext();
            var academyId = ctx.Academies.FirstOrDefault(a => a.Name == model.AcademyName).AcademyId;
            var programId = ctx.Programs.FirstOrDefault(a => a.Name == model.ProgramName).ProgramId;
            Instructor entity = new Instructor
            {
                FullName = model.FullName,
                ProgramName = model.ProgramName,
                ProgramId = programId,
                AcademyName = model.AcademyName,
                AcademyId = academyId
            };
            _context.Instructors.Add(entity);
            var changeCount = _context.SaveChanges();

            return changeCount == 1;
        }
        public List<InstructorListItem> GetAllInstructors()
        {
            var entityList = _context.Instructors.ToList();

            var programList = entityList.Select(p => new InstructorListItem
            {
                InstructorId = p.InstructorId,
                FullName = p.FullName,
                AcademyId = p.AcademyId,
                AcademyName = p.AcademyName,
                ProgramId = p.ProgramId,
                ProgramName = p.ProgramName,
                AverageRating = p.AverageRating
            }).ToList();
            return programList;
        }
        public InstructorEdit GetInstructorById(int id)
        {
            var model = new InstructorEdit();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Instructors.FirstOrDefault(a => a.InstructorId == id);
                model.InstructorId = id;
                model.FullName = entity.FullName;
                model.ProgramId = entity.ProgramId;
                model.ProgramName = entity.ProgramName;
                model.AcademyId = entity.AcademyId;
                model.AcademyName = entity.AcademyName;
            }
            return model;
        }
        public InstructorDetail GetInstructorByDetail(int id)
        {
            var model = new InstructorDetail();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Instructors.FirstOrDefault(i => i.InstructorId == id);
                model.InstructorId = entity.InstructorId;
                model.FullName = entity.FullName;
                model.ProgramId = entity.ProgramId;
                model.ProgramName = entity.ProgramName;
                model.AcademyId = entity.AcademyId;
                model.AcademyName = entity.AcademyName;
                model.Ratings = entity.Ratings;
            }
            return model;
        }
        public InstructorEdit GetInstructorByName(string name)
        {
            var model = new InstructorEdit();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = _context.Instructors.FirstOrDefault(i => i.FullName == name);
                model.FullName = entity.FullName;
                model.ProgramId = entity.ProgramId;
                model.AcademyId = entity.AcademyId;
            }
            return model;
        }
        public string GetAcademyNameById(int id)
        {
            var ctx = new ApplicationDbContext();
            var entity = ctx.Academies.Find(id);
            string academyName = entity.Name;

            return academyName;
        }
        public string GetProgramNameById(int id)
        {
            var ctx = new ApplicationDbContext();
            var entity = ctx.Programs.Find(id);
            string programName = entity.Name;

            return programName;
        }
        public bool UpdateInstructor(InstructorEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Instructors.Single(p => p.FullName == model.FullName);
                var academyId = ctx.Academies.Single(a => a.Name == model.AcademyName).AcademyId;
                var programId = ctx.Programs.Single(a => a.Name == model.ProgramName).ProgramId;

                entity.FullName = model.FullName;
                entity.ProgramId = academyId;
                entity.AcademyId = programId;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteInstructorById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Instructors.Single(p => p.InstructorId == id);

                ctx.Instructors.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
