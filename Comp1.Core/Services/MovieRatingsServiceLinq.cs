using Comp1.Core.Interfaces;
using Comp1.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;

namespace Comp1.Core.Services
{
    public class MovieRatingsServiceLinq : IMovieRatingsService
    {
        private IMovieRatingsRepository Repository;

        public MovieRatingsServiceLinq(IMovieRatingsRepository repo)
        {
            Repository = repo ?? throw new ArgumentException("Missing MovieRatings repository");
        }

        public double GetAverageRateFromReviewer(int reviewer)
        {
            try
            {
                return Repository.Ratings
                    .Where<MovieRating>(rating => rating.Reviewer == reviewer)
                    .Average<MovieRating>(rating => rating.Grade);
            }
            catch
            {
                throw new ArgumentException($"Reviewer:{reviewer} has no reviews");
            }
        }

        public double GetAverageRateOfMovie(int movie)
        {
            try
            {
                return Repository.Ratings
                    .Where<MovieRating>(rating => rating.Movie == movie)
                    .Average<MovieRating>(rating => rating.Grade);
            }
            catch
            {
                throw new ArgumentException($"Movie:{movie} has no reviews");
            }
        }

        public List<int> GetMostProductiveReviewers()
        {
            var maxCount = Repository.Ratings
                .GroupBy(x => x.Reviewer).Max(grp => grp.Count());

            return Repository.Ratings
               .GroupBy(rating => rating.Reviewer)
               .Select(group => new
               {
                   reviewer = group.Key,
                   count = group.Count()
               })
               .Where(x => x.count == maxCount)
               .Select(x => x.reviewer)
               .ToList();
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            var movie5 = Repository.Ratings.AsParallel()
                .Where(r => r.Grade == 5)
                .GroupBy(r => r.Movie)
                .Select(group => new
                {
                    Movie = group.Key,
                    TopRates = group.Count()
                });

            int maxTopRates = movie5.Max(Group => Group.TopRates);
            
            return movie5
                .Where(grp => grp.TopRates == maxTopRates)
                .Select(grp => grp.Movie)
                .ToList();
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            return Repository.Ratings
                .Where<MovieRating>(rating => rating.Movie == movie && rating.Grade == rate)
                .Count();
        }

        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            return Repository.Ratings
                 .Where<MovieRating>(rating => rating.Reviewer == reviewer && rating.Grade == rate)
                 .Count();
        }

        public int GetNumberOfReviews(int movie)
        {
            return Repository.Ratings
                .Where(rating => rating.Movie == movie)
                .Count();
        }

        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            return Repository.Ratings
                .Where(rating => rating.Reviewer == reviewer)
                .Count();
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            return Repository.Ratings
                .Where(r => r.Movie == movie)
                .OrderByDescending(r => r.Grade)
                .ThenBy(r => r.Date)
                .Select(r => r.Reviewer)
                .ToList();
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            return Repository.Ratings
                .Where (r => r.Reviewer == reviewer)
                .OrderByDescending(r => r.Grade)
                .ThenBy(r => r.Date)
                .Select(r => r.Movie)
                .ToList();
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            return Repository.Ratings
                .GroupBy(r => r.Movie)
                .Select(grp => new {
                    Movie = grp.Key,
                    AvgGrade = grp.Average(x => x.Grade)
                })
                .OrderByDescending(grp => grp.AvgGrade)
                .Select(grp => grp.Movie)
                .Take(amount)
                .ToList();
        }
    }
}
