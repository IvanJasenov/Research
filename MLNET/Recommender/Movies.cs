using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Recommender
{
    /// <summary>
    /// The Movie class holds a single movie.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The movie identifier.
        /// </summary>
        public int ID;

        /// <summary>
        /// The movie title.
        /// </summary>
        public String Title;
    }

    /// <summary>
    /// The Movies class holds the entire list of movies.
    /// </summary>
    public static class Movies
    {
        /// <summary>
        /// The full movie database.
        /// </summary>
        public static List<Movie> All = new List<Movie>();

        // private members
        private static string moviesDataPath = Path.Combine(Environment.CurrentDirectory, "recommendation-movies.csv");

        /// <summary>
        /// Initialize the static class members.
        /// </summary>
        static Movies()
        {
            All = LoadMovieData(moviesDataPath);
        }

        /// <summary>
        /// Get a single movie.`
        /// </summary>
        /// <param name="id">The identifier of the movie to get.</param>
        /// <returns>The Movie instance corresponding to the specified identifier.</returns>        
        public static Movie Get(int id)
        {
            return All.Single(m => m.ID == id);
        }

        /// <summary>
        /// Load the entire movie list in memory.
        /// </summary>
        /// <param name="moviesdatasetpath">The path to the movie csv file.</param>
        /// <returns>A List instance containing the entire movie list.</returns>
        private static List<Movie> LoadMovieData(String moviesdatasetpath)
        {
            var result = new List<Movie>();
            Stream fileReader = File.OpenRead(moviesdatasetpath);
            StreamReader reader = new StreamReader(fileReader);
            try
            {
                bool header = true;
                int index = 0;
                var line = "";
                while (!reader.EndOfStream)
                {
                    if (header)
                    {
                        line = reader.ReadLine();
                        header = false;
                    }
                    line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    int movieId = Int32.Parse(fields[0].ToString().TrimStart(new char[] { '0' }));
                    string movieTitle = fields[1].ToString();
                    result.Add(new Movie() { ID = movieId, Title = movieTitle });
                    index++;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return result;
        }
    }
}
