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
        public async Task<bool> CreateProgramAsync(ProgramCreate model)
        {
            //var query = await _context.Academies.Select(a => a.Name == model.AcademyName).ToListAsync();
            //int academyId = query.
             Program entity = new Program
            {
                Name = model.Name,
                Type = model.Type,
                Prerequisite = model.Prerequisite,
                AcademyId = model.AcademyId
            };
            _context.Programs.Add(entity);
            var changeCount = await _context.SaveChangesAsync();

            return changeCount == 1;
        }
        public async Task<List<ProgramListItem>> GetAllProgramsAsync()
        {
            var entityList = await _context.Programs.ToListAsync();

            var programList = entityList.Select(p => new ProgramListItem
            {
                ProgramId = p.ProgramId,
                Name = p.Name,
                Type = p.Type,
                Prerequisite = p.Prerequisite,
                AverageRating = p.AverageRating
            }).ToList();
            return programList;
        }
        //GetProgramById
        public ProgramDetail GetProgramById(int id)
        {
            var model = new ProgramDetail();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.FirstOrDefault(a => a.ProgramId == id);
                entity.Name = model.Name;
                entity.Type = model.Type;
                entity.Prerequisite = model.Prerequisite;
                entity.AcademyId = model.AcademyId;
                entity.Academy = model.Academy;
            }
            return model;
        }
        //UpdateProgram
        public bool UpdateProgramByIdAsync(ProgramEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.Single(p => p.ProgramId == model.ProgramId);

                entity.ProgramId = model.ProgramId;
                entity.Name = model.Name;
                entity.Type = model.Type;
                entity.Prerequisite = model.Prerequisite;
                entity.AcademyId = model.AcademyId;
                entity.Academy = model.Academy;

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
