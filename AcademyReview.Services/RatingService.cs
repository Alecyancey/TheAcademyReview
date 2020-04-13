using AcademyReview.Data;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using AcademyReview.Models.RatingModels.Instructor;
using AcademyReview.Models.RatingModels.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Services
{
    public class RatingService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;
        public RatingService(string userId)
        {
            _context = new ApplicationDbContext();
            _userId = userId;
        }
        public List<ProgramRatingListItem> GetAnProgramRatings(int id)
        {
            //var entityList = _context.InstructorRatings.ToList();
            var programList = _context.ProgramRatings.Where(i => i.ProgramId == id);
            var ratingList = programList.Select(p => new ProgramRatingListItem
            {
                ProgramId = p.ProgramId,
                ProgramName = p.ProgramName,
                RatingId = p.RatingId,
                Score = p.Score,
                Description = p.Description,
                UserId = p.UserId,
            }).ToList();

            return ratingList;
        }
        public List<InstructorRatingListItem> GetAnInstructorRatings(int id)
        {
            //var entityList = _context.InstructorRatings.ToList();
            var instructorList = _context.InstructorRatings.Where(i => i.InstructorId == id);

            var programList = instructorList.Select(p => new InstructorRatingListItem
            {
                InstructorId = p.InstructorId,
                FullName = p.FullName,
                RatingId = p.RatingId,
                Score = p.Score,
                Description = p.Description,
                UserId = p.UserId,
            }).ToList();
            return programList;
        }
        public List<AcademyRatingListItem> GetAnAcademyRatings(int id)
        {
            //var entityList = _context.InstructorRatings.ToList();
            var academyList = _context.AcademyRatings.Where(i => i.AcademyId == id);
            var ratingList = academyList.Select(p => new AcademyRatingListItem
            {
                AcademyId = p.AcademyId,
                AcademyName = p.AcademyName,
                RatingId = p.RatingId,
                Score = p.Score,
                Description = p.Description,
                UserId = p.UserId,
            }).ToList();
            
            return ratingList;
        }
        public async Task<bool> CreateAcademyRatingAsync(AcademyRatingCreate model)
        {
            var entity = new AcademyRating
            {
                Description = model.Description,
                AcademyId = model.AcademyId,
                Score = model.Score,
                UserId = _userId
            };

            _context.Ratings.Add(entity);
            var changeCount = await _context.SaveChangesAsync();

            return changeCount == 1;
        }
        public async Task<bool> CreateProgramRatingAsync(ProgramRatingCreate model)
        {
            var entity = new ProgramRating
            {
                Description = model.Description,
                ProgramId = model.ProgramId,
                Score = model.Score,
                UserId = _userId
            };

            _context.Ratings.Add(entity);
            var changeCount = await _context.SaveChangesAsync();

            return changeCount == 1;
        }
        public bool CreateInstructorRating(InstructorRatingCreate model)
        {
            var entity = new InstructorRating
            {
                Description = model.Description,
                InstructorId = model.InstructorId,
                Score = model.Score,
                //FullName = model.FullName,
                //ProgramId = model.ProgramId,
                //AcademyId = model.AcademyId,
                //AcademyName = model.AcademyName,
                UserId = _userId
            };

            var ctx = new ApplicationDbContext();
            ctx.Ratings.Add(entity);
            return ctx.SaveChanges() == 1;
            //_context.Ratings.Add(entity);
            //var changeCount = _context.SaveChangesAsync();

            //return changeCount == 1;
        }
        //public async Task<bool> CreateInstructorRatingAsync(InstructorRatingCreate model)
        //{
        //    var entity = new InstructorRating
        //    {
        //        Description = model.Description,
        //        InstructorId = model.InstructorId,
        //        Score = model.Score,
        //        UserId = _userId
        //    };

        //    _context.Ratings.Add(entity);
        //    var changeCount = await _context.SaveChangesAsync();

        //    return changeCount == 1;
        //}
    }
}
