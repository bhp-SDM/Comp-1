using Comp1.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRatingsJSONRepository;
using System.Linq;

namespace MSUnitTestProject1
{
    [TestClass]
    public class MovieRatingsServiceLinqPerformanceTest
    {

        private static MovieRatingsServiceLinq service;

        // Reveiwer with most reviews - worst case in dataset
        private static int reviewerMostReviews;
        
        // Movie with most reviews - worst case in dataset
        private static int movieMostReviews;

        [ClassInitialize]
        public static void SetUpTest(TestContext tc)
        {
            MovieRatingsRepository repo = new MovieRatingsRepository(@"C:\Users\bhp\source\repos\PP\2020E\Compulsory\ratings.json");

            reviewerMostReviews = GetReviewerMostReviews(repo);
            movieMostReviews = GetMovieMostReviews(repo);

            service = new MovieRatingsServiceLinq(repo);
        }

        private static int GetMovieMostReviews(MovieRatingsRepository repo)
        {
            return repo.Ratings
                .GroupBy(r => r.Movie)
                .Select(grp => new
                {
                    Movie = grp.Key,
                    Reviews = grp.Count()
                })
                .OrderByDescending(grp => grp.Reviews)
                .Select(grp => grp.Movie)
                .FirstOrDefault();
        }

        private static int GetReviewerMostReviews(MovieRatingsRepository repo)
        {
            return repo.Ratings
                .GroupBy(r => r.Reviewer)
                .Select(grp => new
                {
                    Reviewer = grp.Key,
                    Reviews = grp.Count()
                })
                .OrderByDescending(grp => grp.Reviews)
                .Select(grp => grp.Reviewer)
                .FirstOrDefault();
        }

        //  1. On input N, what are the number of reviews from reviewer N?
        [TestMethod]
        [Timeout(4000)]
        public void GetNumberofReviewsFromReviewer()
        {
            service.GetNumberOfReviewsFromReviewer(reviewerMostReviews);
        }

        //  2. On input N, what is the average rate that reviewer N had given?
        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateFromReviewer()
        {
            service.GetNumberOfReviewsFromReviewer(reviewerMostReviews);
        }

        //  3. On input N and R, how many times has reviewer N given rate R?
        [TestMethod]
        [Timeout(4000)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void GetNumberOfRatesByReviewer(int rate)
        {
            service.GetNumberOfRatesByReviewer(reviewerMostReviews, rate);
        }

        //  4. On input N, how many have reviewed movie N?
        [TestMethod]
        [Timeout(4000)]
        public void GetNumberOfReviews()
        {
            service.GetNumberOfReviews(movieMostReviews);
        }

        //  5. On input N, what is the average rate the movie N had received?
        [TestMethod]
        [Timeout(4000)]
        public void GetAverageRateOfMovie()
        {
            service.GetAverageRateOfMovie(movieMostReviews);
        }

        //  6. On input N and R, how many times had movie N received rate R?
        [TestMethod]
        [Timeout(4000)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void GetNumberOfRates(int rate)
        {
            service.GetNumberOfRates(movieMostReviews, rate);
        }

        //  7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        [TestMethod]
        [Timeout(4000)]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            service.GetMoviesWithHighestNumberOfTopRates();
        }

        //  8. What reviewer(s) had done most reviews?
        [TestMethod]
        [Timeout(4000)]
        public void GetMostProductiveReviewers()
        {
            service.GetMostProductiveReviewers();
        }

        //  9. On input N, what is top N of movies? The score of a movie is its average rate.
        [TestMethod]
        [Timeout(4000)]
        public void GetTopRatedMovies()
        {
            service.GetTopRatedMovies(10);
        }

        //  10. On input N, what are the movies that reviewer N has reviewed? 
        //      The list should be sorted decreasing by rate first, and date secondly.
        [TestMethod]
        [Timeout(4000)]
        public void GetTopMoviesByReviewer()
        {
            service.GetTopMoviesByReviewer(reviewerMostReviews);
        }

        //  11. On input N, who are the reviewers that have reviewed movie N? 
        //      The list should be sorted decreasing by rate first, and date secondly.
        [TestMethod]
        [Timeout(4000)]
        public void GetReviewersByMovie()
        {
            service.GetReviewersByMovie(movieMostReviews);
        }

    }
}
