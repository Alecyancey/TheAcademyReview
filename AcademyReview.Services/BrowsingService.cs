using AcademyReview.Data;
using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.InstructorModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using AcademyReview.Models.RatingModels.Instructor;
using AcademyReview.Models.RatingModels.Program;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AcademyReview.Services
{
    public class BrowsingService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;
        public BrowsingService(string userId)
        {
            _context = new ApplicationDbContext();
            _userId = userId;
        }
        public bool CreateAcademy(AcademyCreate model)
        {
            var ctx = new ApplicationDbContext();
            string createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
            Academy entity = new Academy
            {
                Name = model.Name,
                City = model.City,
                State = model.State,
                OwnerId = _userId,
                CreatedBy = createdBy
            };
            _context.Academies.Add(entity);
            var changeCount = _context.SaveChanges();

            return changeCount == 1;
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
        public bool CreateInstructor(InstructorCreate model)
        {
            var ctx = new ApplicationDbContext();
            var academyId = ctx.Academies.FirstOrDefault(a => a.Name == model.AcademyName).AcademyId;
            var programId = ctx.Programs.FirstOrDefault(a => a.Name == model.ProgramName).ProgramId;
            string createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
            Instructor entity = new Instructor
            {
                FullName = model.FullName,
                ProgramName = model.ProgramName,
                ProgramId = programId,
                AcademyName = model.AcademyName,
                AcademyId = academyId,
                OwnerId = _userId,
                CreatedBy = createdBy
            };
            _context.Instructors.Add(entity);
            var changeCount = _context.SaveChanges();

            return changeCount == 1;
        }
        public async Task<List<AcademyListItem>> GetBrowsingAcademies()
        {
            var entityList = await _context.Academies.ToListAsync();

            var academyList = entityList.Select(a => new AcademyListItem
            {
                AcademyId = a.AcademyId,
                Name = a.Name,
                City = a.City,
                State = a.State,
                AverageRating = a.AverageRating
            }).ToList();
            var sortedList = academyList.OrderBy(a => a.Name).ToList();
            //sortedList.Reverse();
            return sortedList;
        }
        public List<ProgramListItem> GetAcademysPrograms(int id)
        {
            var ctx = new ApplicationDbContext();
            var programs = ctx.Programs.Where(p => p.AcademyId == id).ToList();
            var programList = programs.Select(a => new ProgramListItem
            {
                ProgramId = a.ProgramId,
                AcademyId = a.AcademyId,
                Name = a.Name,
                AverageRating = a.AverageRating,
                AcademyName = a.AcademyName
            }).ToList();
            var sortedList = programList.OrderBy(p => p.Name).ToList();

            return sortedList;

        }
        public List<InstructorListItem> GetProgramsInstructors(int id)
        {
            var ctx = new ApplicationDbContext();
            var instructors = ctx.Instructors.Where(i => i.ProgramId == id).ToList();
            var instructorList = instructors.Select(i => new InstructorListItem
            {
                InstructorId = i.InstructorId,
                FullName = i.FullName,
                AverageRating = i.AverageRating,
                AcademyName = i.AcademyName,
                ProgramName = i.ProgramName
            }).ToList();
            var sortedList = instructorList.OrderBy(i => i.FullName).ToList();

            return sortedList;
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
                model.CreatedBy = entity.CreatedBy;
            }
            return model;
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
        public bool DeleteRating(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(p => p.RatingId == id);

                ctx.Ratings.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
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
                model.Ratings = entity.Ratings;

                //model.Programs = entity.Programs;
                //model.Instructors = entity.Instructors;
                //model.Ratings = entity.Ratings;
            }
            return model;
        }
        public bool UpdateProgram(ProgramEdit model)
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
        public bool DeleteProgramById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Programs.Single(p => p.ProgramId == id);

                ctx.Programs.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(p => p.RatingId == id);

                ctx.Ratings.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public AcademyDetail GetAcademyByDetail(int id)
        {
            var model = new AcademyDetail();
            var ctx = new ApplicationDbContext();
            using (ctx)
            {
                string createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
                var entity = ctx.Academies.FirstOrDefault(i => i.AcademyId == id);
                //var programs = ctx.Programs.Where(p => p.AcademyId == id);
                model.AcademyId = entity.AcademyId;
                model.Name = entity.Name;
                model.State = entity.State;
                model.City = entity.City;
                model.CreatedBy = entity.CreatedBy;
                model.Ratings = entity.Ratings;
                //model.Programs = entity.Programs;
                //model.Instructors = entity.Instructors;
            }
            return model;
        }
        public AcademyDetail GetAcademyById(int id)
        {
            var model = new AcademyDetail();
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Academies.FirstOrDefault(a => a.AcademyId == id);
                entity.Name = model.Name;
                entity.City = model.City;
                entity.State = model.State;
            }
            return model;
        }
        public bool UpdateAcademy(AcademyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Academies.Single(p => p.AcademyId == model.AcademyId);

                entity.Name = model.Name;
                entity.City = model.City;
                entity.State = model.State;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteAcademyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Academies.Single(p => p.AcademyId == id);

                ctx.Academies.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
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
                UserId = p.OwnerId,
                CreatedBy = p.CreatedBy
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
                UserId = p.OwnerId,
                CreatedBy = p.CreatedBy
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
                UserId = p.OwnerId,
                CreatedBy = p.CreatedBy
            }).ToList();

            return ratingList;
        }
        public bool CreateAcademyRating(AcademyRatingCreate model)
        {
            var ctx = new ApplicationDbContext();
            var createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
            var entity = new AcademyRating
            {
                Description = model.Description,
                AcademyId = model.AcademyId,
                Score = model.Score,
                OwnerId = _userId,
                CreatedBy = createdBy
            };

            _context.Ratings.Add(entity);
            var changeCount = _context.SaveChanges();

            return changeCount == 1;
        }
        public bool CreateProgramRating(ProgramRatingCreate model)
        {
            var ctx = new ApplicationDbContext();
            var createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
            var entity = new ProgramRating
            {
                Description = model.Description,
                ProgramId = model.ProgramId,
                Score = model.Score,
                OwnerId = _userId,
                CreatedBy = createdBy
            };

            _context.Ratings.Add(entity);
            var changeCount = _context.SaveChanges();

            return changeCount == 1;
        }
        public bool CreateInstructorRating(InstructorRatingCreate model)
        {
            var ctx = new ApplicationDbContext();
            var createdBy = ctx.Users.FirstOrDefault(u => u.Id == _userId).UserName;
            var entity = new InstructorRating
            {
                Description = model.Description,
                InstructorId = model.InstructorId,
                Score = model.Score,
                //FullName = model.FullName,
                //ProgramId = model.ProgramId,
                //AcademyId = model.AcademyId,
                //AcademyName = model.AcademyName,
                OwnerId = _userId,
                CreatedBy = createdBy
            };

            ctx.Ratings.Add(entity);
            return ctx.SaveChanges() == 1;
            //_context.Ratings.Add(entity);
            //var changeCount = _context.SaveChangesAsync();

            //return changeCount == 1;
        }
        public bool UpdateRating(RatingEdit model)
        {
            var ctx = new ApplicationDbContext();
            var rating = ctx.Ratings.FirstOrDefault(r => r.RatingId == model.Id);
            rating.RatingId = model.Id;
            rating.Score = model.Score;
            rating.Description = model.Description;

            return ctx.SaveChanges() == 1;
        }
    }
}
