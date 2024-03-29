﻿using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class LikesRepository : ILikesRepository
    {
        private JLDatabaseContext jLDatabaseContext;

        public LikesRepository(JLDatabaseContext jLDatabaseContext)
        {
            this.jLDatabaseContext = jLDatabaseContext;
        }

        public async Task<List<Likes>> GetAllLikesOfReviews(int reviewId)
        {
            if (
                    await jLDatabaseContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId) == null
               )
            {
                return null;
            }
            return await jLDatabaseContext.Likes.Where(x => x.ReviewId == reviewId).ToListAsync();
        }

        public async Task<int> AddNewLike(Likes like, int userId)
        {
            like.UserId = userId;
            Likes checkLike = await jLDatabaseContext.Likes
                              .FirstOrDefaultAsync(x => x.UserId == userId && x.ReviewId == like.ReviewId);

            //Check an existence of like in database.
            if (checkLike != null)
            {
                CheckExistenceOfLike(checkLike, like);
                await jLDatabaseContext.SaveChangesAsync();
                return like.Id; 
            }

            await jLDatabaseContext.AddAsync(like);
            await jLDatabaseContext.SaveChangesAsync();
            return like.Id;
        }

        private void CheckExistenceOfLike(Likes checkLike, Likes like)
        {
            //Deleting like if uploading like has equal value of IsLike
            if (like.IsLike.Equals(checkLike.IsLike))
            {
                jLDatabaseContext.Likes.Remove(checkLike);
            }
            //Updating like if uploading like has unequal value of IsLike
            else
            {
                checkLike.IsLike = like.IsLike;
            }
        }
    }
}
