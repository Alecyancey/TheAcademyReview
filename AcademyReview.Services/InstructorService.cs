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
        public async Task<bool> CreateInstructorAsync(InstructorCreate model)
        {
            Instructor entity = new Instructor
            {
                FullName = model.FullName,
                ProgramId = model.ProgramId,
                AcademyId = model.AcademyId,
            };
            _context.Instructors.Add(entity);
            var changeCount = await _context.SaveChangesAsync();

            return changeCount == 1;
        }
        public List<InstructorListItem> GetAllInstructors()
        {
            var entityList = _context.Instructors.ToList();

            var programList = entityList.Select(p => new InstructorListItem
            {
                FullName = p.FullName,
                AverageRating = p.AverageRating
            }).ToList();
            return programList;
        }
        public Instructor GetInstructorById(int id)
        {
            var model = new Instructor();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Instructors.FirstOrDefault(a => a.InstructorId == id);
                entity.FullName = model.FullName;
                entity.ProgramId = model.ProgramId;
                entity.AcademyId = model.AcademyId;
            }
            return model;
        }
        public bool UpdateInstructorById(InstructorEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Instructors.Single(p => p.InstructorId == model.InstructorId);

                entity.InstructorId = model.InstructorId;
                entity.FullName = model.FullName;
                entity.ProgramId = model.ProgramId;
                entity.AcademyId = model.AcademyId;

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
