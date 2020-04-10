﻿using AcademyReview.Data;
using AcademyReview.Models.AcademyModels;
using AcademyReview.Models.ProgramModels;
using AcademyReview.Models.RatingModels.Academy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyReview.Services
{
    public class AcademyService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _userId;
        public AcademyService(string userId)
        {
            _context = new ApplicationDbContext();
            _userId = userId;
        }
        public async Task<bool> CreateAcademyAsync(AcademyCreate model)
        {
            Academy entity = new Academy
            {
                Name = model.Name,
                City = model.City,
                State = model.State
            };
            _context.Academies.Add(entity);
            var changeCount = await _context.SaveChangesAsync();

            return changeCount == 1;
        }
        public async Task<List<AcademyListItem>> GetAllAcademiesAsync()
        {
            var entityList = await _context.Academies.ToListAsync();

            var academyList = entityList.Select(a => new AcademyListItem
            {
                AcademyId = a.AcademyId,
                Name = a.Name,
                City = a.City,
                State = a.State,
                AverageRating = a.AverageRating,
                ProgramCount = a.Programs.Count
            }).ToList();
            return academyList;
        }

        //public async Task<AcademyDetail> GetAcademyByIdAsync(int academyId)
        //{
        //    var entity = await _context.Academies.FindAsync(academyId);
        //    if (entity == null)
        //        return null;

        //    var model = new AcademyDetail
        //    {
        //        AcademyId = entity.AcademyId,
        //        Name = entity.Name,
        //        City = entity.City,
        //        State = entity.State,
        //        Programs = entity.Programs.Select(program => new ProgramListItem
        //        {
        //            ProgramId = program.ProgramId,
        //            Name = program.Name,
        //            Type = program.Type,
        //            AverageRating = program.AverageRating
        //        }).ToList(),
        //        Ratings = new List<AcademyRatingListItem>()
        //    };

        //    foreach (var rating in entity.Ratings)
        //    {
        //        model.Ratings.Add(new AcademyRatingListItem
        //        {
        //            RatingId = rating.RatingId,
        //            AcademyId = entity.AcademyId,
        //            AcademyName = entity.Name,
        //            Description = rating.Description,
        //            IsUserOwned = rating.UserId == _userId,
        //            Score = rating.Score
        //            //VisitDate = rating.VisitDate
        //        });
        //    }
        //    return model;
        //}
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
        //UpdateAcademy
        public bool UpdateAcademyByIdAsync(AcademyEdit model)
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
        //DeleteAcademy
        public bool DeleteAcademyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Academies.Single(p => p.AcademyId == id);

                ctx.Academies.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}