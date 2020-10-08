using Comp1.Core.Interfaces;
using Comp1.Core.Model;
using Comp1.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingsServiceTest
    {
        private MovieRating[] ratings = null;
        private readonly Mock<IMovieRatingsRepository> repoMock = null;

        public MovieRatingsServiceTest()
        {
            repoMock = new Mock<IMovieRatingsRepository>();
            repoMock.Setup(repo => repo.Ratings).Returns(() => ratings);
        }

        [Fact]
        public void CreateMovieRatingsService()
        {
            var mrs = new MovieRatingsService(repoMock.Object);
            mrs.Should().NotBeNull();
        }

        [Fact]
        public void CreateMovieRatingsServiceMovieRatingsRepositoryIsNullExpectArgumentException()
        {
            MovieRatingsService mrs = null;

            Action ac = () => mrs = new MovieRatingsService(null);

            ac.Should().Throw<ArgumentException>().WithMessage("Missing MovieRatings repository");
            mrs.Should().BeNull();
        }

        // 1.  On input N, what are the number of reviews from reviewer N?

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void GetNumberOfReviewsFromReviewer(int movie, int expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 3, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetNumberOfReviewsFromReviewer(movie);

            Assert.Equal(expected, result);
        }

        // 2. On input N, what is the average rate that reviewer N had given?

        [Theory]
        [InlineData(2, 3)]
        [InlineData(3, 3.5)]
        public void GetAverageRateFromReviewerWithReviews(int reviewer, double expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 3, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetAverageRateFromReviewer(reviewer);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAverageRatingFromReviewerWithNoReviewsExpectArgumentException()
        {
            int reviewer = 2;

            ratings = new MovieRating[]
            {
                new MovieRating(3, 1, 3, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            Action ac = () => mrs.GetAverageRateFromReviewer(reviewer);

            ac.Should().Throw<ArgumentException>().WithMessage($"Reviewer:{reviewer} has no reviews");
        }


        // 3. On input N and R, how many times has reviewer N given rate R?

        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(2, 3, 1)]
        [InlineData(2, 4, 2)]
        public void GetNumberOfRatesByReviewer(int reviewer, int rate, int expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),
                new MovieRating(2, 3, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetNumberOfRatesByReviewer(reviewer, rate);

            Assert.Equal(expected, result);
        }

        //  4. On input N, how many have reviewed movie N?

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void GetNumberOfReviews(int movie, int expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(1, 2, 3, DateTime.Now),
                new MovieRating(2, 3, 3, DateTime.Now),
                new MovieRating(3, 3, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetNumberOfReviews(movie);

            Assert.Equal(expected, result);
        }

        //  5. On input N, what is the average rate the movie N had received? 
        [Theory]
        [InlineData(2, 3)]
        [InlineData(3, 3.5)]
        public void GetAverageRateOfMovieWithReviews(int movie, double expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(1, 2, 3, DateTime.Now),
                new MovieRating(2, 3, 3, DateTime.Now),
                new MovieRating(2, 3, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetAverageRateOfMovie(movie);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAverageRatingOfMovieWithNoReviewsExpectArgumentException()
        {
            int movie = 2;

            ratings = new MovieRating[]
            {
                new MovieRating(3, 1, 3, DateTime.Now),
                new MovieRating(3, 3, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            Action ac = () => mrs.GetAverageRateOfMovie(movie);

            ac.Should().Throw<ArgumentException>().WithMessage($"Movie:{movie} has no reviews");
        }

        //  6. On input N and R, how many times had movie N received rate R?

        [Theory]
        [InlineData(2, 2, 0)]
        [InlineData(2, 3, 1)]
        [InlineData(2, 4, 2)]
        public void GetNumberOfRates(int movie, int rate, int expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(1, 2, 3, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now)
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetNumberOfRates(movie, rate);

            Assert.Equal(expected, result);
        }


        //  7. What is the id(s) of the movie(s) with the highest number of top rates (5)?

        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            ratings = new MovieRating[]
            {
                new MovieRating(1, 1, 4, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),
                new MovieRating(1, 3, 5, DateTime.Now),
                new MovieRating(2, 3, 5, DateTime.Now),
                new MovieRating(3, 3, 4, DateTime.Now),
                new MovieRating(3, 4, 5, DateTime.Now),
                new MovieRating(3, 4, 5, DateTime.Now),
            };
            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            List<int> expected = new List<int>() { 3, 4 };

            var result = mrs.GetMoviesWithHighestNumberOfTopRates();

            Assert.Equal(expected, result);
        }

        //  8. What reviewer(s) had done most reviews?

        [Fact]
        public void GetMostProductiveReviewers()
        {
            ratings = new MovieRating[]
            {
                new MovieRating(1, 2, 3, DateTime.Now),
                new MovieRating(1, 3, 4, DateTime.Now),
                new MovieRating(2, 3, 1, DateTime.Now),
                new MovieRating(3, 4, 2, DateTime.Now),
                new MovieRating(3, 3, 1, DateTime.Now),
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var expected = new List<int>() { 1, 3 };

            var result = mrs.GetMostProductiveReviewers();

            Assert.Equal(expected, result);

        }

        //  9. On input N, what is top N of movies? 
        //     The score of a movie is its average rate.

        [Theory]
        [InlineData(1, new int[] { 4 })]
        [InlineData(2, new int[] { 4, 1 })]
        [InlineData(4, new int[] { 4, 1, 2, 3 })]
        [InlineData(10, new int[] { 4, 1, 2, 3 })]
        public void GetTopRatedMovies(int n , int[] expected)
        {
            ratings = new MovieRating[]
            {
                new MovieRating(1, 2, 3, DateTime.Now),     // movie 1 avg = 4                                                            
                new MovieRating(1, 3, 2, DateTime.Now),     // movie 2 avg = 3
                new MovieRating(2, 1, 4, DateTime.Now),     // movie 3 avg = 2.5
                new MovieRating(2, 3, 3, DateTime.Now),     // movie 4 avg = 4.5
                new MovieRating(2, 4, 4, DateTime.Now),
                new MovieRating(3, 4, 5, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetTopRatedMovies(n);

            Assert.Equal(expected.ToList(), result);
        }

        //  10. On input N, what are the movies that reviewer N has reviewed? 
        //      The list should be sorted decreasing by rate first, and date secondly.

        [Theory]
        [InlineData(1, 0)]      // expected[0] = {}
        [InlineData(2, 1)]      // expected[1] = {1}
        [InlineData(3, 2)]      // expected[2] = {3, 2, 1}
        public void GetTopMoviesByReviewer(int reviewer, int expectedIndex)
        {
            var expected = new List<int>[]
            {
                new List<int>(),
                new List<int>(){ 1 },
                new List<int>(){ 3, 2, 1}
            };

            ratings = new MovieRating[]
            {
                new MovieRating(2, 1, 1, new DateTime(2020, 1, 1)),
                new MovieRating(3, 1, 3, new DateTime(2020, 1, 1)),
                new MovieRating(3, 2, 4, new DateTime(2020, 1, 2)),
                new MovieRating(3, 3, 4, new DateTime(2020, 1, 1))
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetTopMoviesByReviewer(reviewer);

            Assert.Equal(expected[expectedIndex], result);
        }

        //  11. On input N, who are the reviewers that have reviewed movie N? 
        //      The list should be sorted decreasing by rate first, and date secondly.

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void GetReviewersByMovie(int movie, int expectedIndex)
        {
            var expected = new List<int>[]
             {
                new List<int>(),
                new List<int>(){ 1 },
                new List<int>(){ 3, 2, 1}
             };

            ratings = new MovieRating[]
            {
                new MovieRating(1, 2, 1, new DateTime(2020, 1, 1)),
                new MovieRating(1, 3, 3, new DateTime(2020, 1, 1)),
                new MovieRating(2, 3, 4, new DateTime(2020, 1, 2)),
                new MovieRating(3, 3, 4, new DateTime(2020, 1, 1))
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.GetReviewersByMovie(movie);

            Assert.Equal(expected[expectedIndex], result);
        }
    }
}
