using Comp1.Core.Interfaces;
using MovieRatingsJSONRepository;
using System;
using System.Diagnostics;

namespace JsonReaderConsole
{
    class Program
    {
        const string JSÒN_FILE_NAME = @"C:\Users\bhp\source\repos\SDM\2020E\Compulsory Assignment\ratings.json";
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            IMovieRatingsRepository repo = new MovieRatingsRepository(JSÒN_FILE_NAME);
            sw.Stop();
            Console.WriteLine("Time = {0:f4} seconds", sw.ElapsedMilliseconds / 1000d);
        }
    }
}
