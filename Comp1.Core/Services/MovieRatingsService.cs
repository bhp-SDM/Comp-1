using Comp1.Core.Interfaces;
using Comp1.Core.Model;
using System;
using System.Collections.Generic;

namespace Comp1.Core.Services
{
    public class MovieRatingsService: IMovieRatingsService
    {
        private readonly IMovieRatingsRepository RatingsRepo;

        public MovieRatingsService(IMovieRatingsRepository repo)
        {
            RatingsRepo = repo ?? throw new ArgumentException("Missing MovieRatings repository");
        }

        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            int count = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (rating.Reviewer == reviewer)
                {
                    count++;
                }
            }

            return count;
        }

        public double GetAverageRateFromReviewer(int reviewer)
        {
            int sum = 0;
            int count = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (rating.Reviewer == reviewer)
                {
                    sum += rating.Grade;
                    count++;
                }
            }
            if (count == 0)
            {
                throw new ArgumentException($"Reviewer:{reviewer} has no reviews");
            }

            return (double)sum / count;
        }

        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            int count = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (rating.Reviewer == reviewer && rating.Grade == rate)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetNumberOfReviews(int movie)
        {
            int count = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (rating.Movie == movie)
                {
                    count++;
                }
            }

            return count;
        }

        public double GetAverageRateOfMovie(int movie)
        {
            int sum = 0;
            int count = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (rating.Movie == movie)
                {
                    sum += rating.Grade;
                    count++;
                }
            }
            if (count == 0)
            {
                throw new ArgumentException($"Movie:{movie} has no reviews");
            }

            return (double)sum / count;
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            int count = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (rating.Movie == movie && rating.Grade == rate)
                {
                    count++;
                }
            }
            return count;
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            SortedDictionary<int, int> movie5 = new SortedDictionary<int, int>();
            int max5 = 0;

            foreach (var rating in RatingsRepo.Ratings)
            {
                int movie = rating.Movie;
                if (rating.Grade == 5)
                {
                    if (!movie5.ContainsKey(movie))
                    {
                        movie5[rating.Movie] = 1;
                    }
                    else
                    {
                        movie5[rating.Movie]++;
                    }

                    if (movie5[rating.Movie] > max5)
                    {
                        max5 = movie5[rating.Movie];
                    }
                }
            }

            var result = new List<int>();
            foreach (var kv in movie5)
            {
                if (kv.Value == max5)
                {
                    result.Add(kv.Key);
                }
            }
            return result;
        }

        public List<int> GetMostProductiveReviewers()
        {
            var reviewerCounts = new SortedDictionary<int, int>();
            int maxReviews = 0;
            foreach (var rating in RatingsRepo.Ratings)
            {
                if (!reviewerCounts.ContainsKey(rating.Reviewer))
                {
                    reviewerCounts[rating.Reviewer] = 1;
                }
                else
                {
                    reviewerCounts[rating.Reviewer]++;
                }

                if (reviewerCounts[rating.Reviewer] > maxReviews)
                {
                    maxReviews = reviewerCounts[rating.Reviewer];
                }
            }

            var result = new List<int>();
            foreach (var kv in reviewerCounts)
            {
                if (kv.Value == maxReviews)
                {
                    result.Add(kv.Key);
                }
            }

            return result;
        }

        public List<int> GetTopRatedMovies(int n)
        {
            var movieScoreSum = new Dictionary<int, int>();
            var movieScoreCount = new Dictionary<int, int>();
            
            foreach(var rating in RatingsRepo.Ratings)
            {
                if (!movieScoreSum.ContainsKey(rating.Movie))
                {
                    movieScoreSum.Add(rating.Movie, rating.Grade);
                    movieScoreCount.Add(rating.Movie, 1);
                }
                else
                {
                    movieScoreSum[rating.Movie] += rating.Grade;
                    movieScoreCount[rating.Movie]++;
                }
            }

            var avgScores = new List<KeyValuePair<double, int>>();
            
            foreach (int movie in movieScoreCount.Keys)
            {
                var avgRate = (double) movieScoreSum[movie] / movieScoreCount[movie];
                avgScores.Add(new KeyValuePair<double, int>(avgRate, movie));
            }
            
            avgScores.Sort(delegate (KeyValuePair<double, int> kv1, KeyValuePair<double, int> kv2)
            {
                if (kv1.Key - kv2.Key < 1E-5) return 0;
                else if (kv1.Key < kv2.Key) return 1;
                else return -1;
            });


            List<int> result = new List<int>();
            for (int i = 0; i < Math.Min(n, avgScores.Count); i++)
            {
                result.Add(avgScores[i].Value);
            }

            return result;
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            MovieRating[] ratings = RatingsRepo.Ratings;
            int index = 0;

            var reviewerRatings = new List<MovieRating>();

            while (index < ratings.Length && ratings[index].Reviewer < reviewer) index++;
            while (index < ratings.Length && ratings[index].Reviewer == reviewer)
            {
                reviewerRatings.Add(ratings[index++]);
            }

            reviewerRatings.Sort(delegate(MovieRating first, MovieRating second)
            {
                if (first.Grade == second.Grade)
                {
                    if (first.Date == second.Date) return 0;
                    else if (first.Date < second.Date) return -1;
                    else return 1;
                }
                else return (second.Grade - first.Grade);
            });
            
            var reviewerMoviesSorted = new List<int>();
            foreach (var rating in reviewerRatings)
            {
                reviewerMoviesSorted.Add(rating.Movie);
            }

            return reviewerMoviesSorted;
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            var ratings = RatingsRepo.Ratings;
            int index = 0;

            var movieRatings = new List<MovieRating>();

            while (index < ratings.Length && ratings[index].Movie < movie) index++;
            while (index < ratings.Length && ratings[index].Movie == movie)
            {
                movieRatings.Add(ratings[index++]);
            }

            movieRatings.Sort(delegate(MovieRating first, MovieRating second)
            {
                if (first.Grade == second.Grade)
                {
                    if (first.Date == second.Date) return 0;
                    else if (first.Date < second.Date) return -1;
                    else return 1;
                }
                else return (second.Grade - first.Grade);
            });
            
            var movieReviewersSorted = new List<int>();
            foreach (var rating in movieRatings)
            {
                movieReviewersSorted.Add(rating.Reviewer);
            }

            return movieReviewersSorted;
        }
    }
}
