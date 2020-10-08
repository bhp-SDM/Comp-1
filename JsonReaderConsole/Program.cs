using Comp1.Core.Interfaces;
using MovieRatingsJSONRepository;
using System;

namespace JsonReaderConsole
{
    class Program
    {
        const string JSÒN_FILE_NAME = @"C:\Users\bhp\source\repos\PP\2020E\Compulsory\ratings.json";
        static void Main(string[] args)
        {
            IMovieRatingsRepository repo = new MovieRatingsRepository(JSÒN_FILE_NAME);
        }
    }
}
