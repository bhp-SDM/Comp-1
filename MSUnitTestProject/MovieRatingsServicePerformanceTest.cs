using Comp1.Core.Interfaces;
using Comp1.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRatingsJSONRepository;
using System.Linq;

namespace MSUnitTestProject
{
    [TestClass]
    public class MovieRatingsServicePerformanceTest
    {
        const int TIMEOUT_IN_MILLIS = 4_000;

        const string JSÒN_FILE_NAME = @"C:\Users\bhp\source\repos\PP\2020E\Compulsory\ratings.json";

        private static IMovieRatingsRepository repository;

        private static int reviewerMostReviews;
        private static int movieMostReviews;

        [ClassInitialize]
        public static void SetupTest(TestContext tc)
        {
            repository = new MovieRatingsRepository(JSÒN_FILE_NAME);

            reviewerMostReviews = repository.Ratings
               .GroupBy(r => r.Reviewer)
               .Select(grp => new
               {
                   reviewer = grp.Key,
                   reviews = grp.Count()
               })
               .OrderByDescending(grp => grp.reviews)
               .Select(grp => grp.reviewer)
               .FirstOrDefault();

            movieMostReviews = repository.Ratings
                .GroupBy(r => r.Movie)
                .Select(grp => new
                {
                    movie = grp.Key,
                    reviews = grp.Count()
                })
                .OrderByDescending(grp => grp.reviews)
                .Select(grp => grp.movie)
                .FirstOrDefault();
        }


        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetNumberOfReviewsFromReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);
            mrs.GetNumberOfReviewsFromReviewer(reviewerMostReviews);
        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetAverageRateFromReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetAverageRateFromReviewer(reviewerMostReviews);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetNumberOfRatesByReviewer(int grade)
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetNumberOfRatesByReviewer(reviewerMostReviews, grade);

        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetNumberOfReviews()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetNumberOfReviews(movieMostReviews);
        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetAverageRateOfMovie()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetAverageRateOfMovie(movieMostReviews);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetNumberOfRates(int grade)
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetNumberOfRates(movieMostReviews, grade);
        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetMoviesWithHighestNumberOfTopRates();
        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetMostProductiveReviewers()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetMostProductiveReviewers();
        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetTopRatedMovies()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetTopRatedMovies(20);
        }


        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetTopMoviesByReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetTopMoviesByReviewer(reviewerMostReviews);
        }

        [TestMethod]
        [Timeout(TIMEOUT_IN_MILLIS)]
        public void GetReviewersByMovie()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            mrs.GetReviewersByMovie(movieMostReviews);
        }
    }
}
