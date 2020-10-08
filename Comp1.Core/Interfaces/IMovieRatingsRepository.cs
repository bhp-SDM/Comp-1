using Comp1.Core.Model;

namespace Comp1.Core.Interfaces
{
    public interface IMovieRatingsRepository
    {
        MovieRating[] GetAllMovieRatings();
    }
}
