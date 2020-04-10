using AcademyReview.Data;
using AcademyReview.Models.RatingModels.Academy;
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
    }
}
