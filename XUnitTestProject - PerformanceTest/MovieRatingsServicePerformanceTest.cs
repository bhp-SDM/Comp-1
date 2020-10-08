using Comp1.Core.Interfaces;
using Comp1.Core.Services;
using System;
using System.Diagnostics;
using Xunit;

namespace XUnitTestProject___PerformanceTest
{
    public class MovieRatingsServiceLinqPerformanceTest : IClassFixture<TestFixture>
    {
        private IMovieRatingsRepository repository;

        private int reviewerMostReviews;
        private int movieMostReviews;


        public MovieRatingsServiceLinqPerformanceTest(TestFixture data)
        {
            repository = data.Repository;
            reviewerMostReviews = data.ReviewerMostReviews;
            movieMostReviews = data.MovieMostReviews;
        }

        private double TimeInSeconds(Action ac)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ac.Invoke();
            sw.Stop();
            return sw.ElapsedMilliseconds / 1000d;
        }


        //  1. On input N, what are the number of reviews from reviewer N?

        [Fact]
        public void GetNumberOfReviewsFromReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                int result = mrs.GetNumberOfReviewsFromReviewer(reviewerMostReviews);
            });

            Assert.True(seconds <= 4);
        }

        //  2. On input N, what is the average rate that reviewer N had given?
        
        [Fact]
        public void GetAverageRateFromReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                double result = mrs.GetAverageRateFromReviewer(reviewerMostReviews);
            });

            Assert.True(seconds <= 4);
        }

        //  3. On input N and R, how many times has reviewer N given rate R?

        [Fact]
        public void GetNumberOfRatesByReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                int result = mrs.GetNumberOfRatesByReviewer(reviewerMostReviews, 3);
            });

            Assert.True(seconds <= 4);
        }

        //  4. On input N, how many have reviewed movie N?

        [Fact]
        public void GetNumberOfReviews()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                int result = mrs.GetNumberOfReviews(movieMostReviews);
            });

            Assert.True(seconds <= 4);
        }

        //  5. On input N, what is the average rate the movie N had received?

        [Fact]
        public void GetAverageRateOfMovie()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                double result = mrs.GetAverageRateOfMovie(movieMostReviews);
            });

            Assert.True(seconds <= 4);
        }

        //  6. On input N and R, how many times had movie N received rate R?

        [Fact]
        public void GetNumberOfRates()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                double result = mrs.GetNumberOfRates(movieMostReviews, 3);
            });

            Assert.True(seconds <= 4);
        }

        //  7. What is the id(s) of the movie(s) with the highest number of top rates (5)?

        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                mrs.GetMoviesWithHighestNumberOfTopRates();
            });

            Assert.True(seconds <= 4);
        }

        //  8. What reviewer(s) had done most reviews?

        [Fact]
        public void GetMostProductiveReviewers()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                var result = mrs.GetMostProductiveReviewers();
            });

            Assert.True(seconds <= 4);
        }

        //  9. On input N, what is top N of movies? The score of a movie is its average rate.

        [Fact]
        public void GetTopRatedMovies()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                var result = mrs.GetTopRatedMovies(100);
            });

            Assert.True(seconds <= 4);
        }

        //  10. On input N, what are the movies that reviewer N has reviewed? 
        //  The list should be sorted decreasing by rate first, and date secondly.

        [Fact]
        public void GetTopMoviesByReviewer()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                var result = mrs.GetTopMoviesByReviewer(reviewerMostReviews);
            });

            Assert.True(seconds <= 4);
        }

        //  11. On input N, who are the reviewers that have reviewed movie N? 
        //      The list should be sorted decreasing by rate first, and date secondly.

        [Fact]
        public void GetReviewersByMovie()
        {
            IMovieRatingsService mrs = new MovieRatingsServiceLinq(repository);

            double seconds = TimeInSeconds(() =>
            {
                var result = mrs.GetReviewersByMovie(movieMostReviews);
            });

            Assert.True(seconds <= 4);
        }

    }
}
