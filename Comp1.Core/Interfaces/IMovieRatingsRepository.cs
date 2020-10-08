using Comp1.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comp1.Core.Interfaces
{
    public interface IMovieRatingsRepository
    {
        MovieRating[] Ratings { get; }            
    }
}
