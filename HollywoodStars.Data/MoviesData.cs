using System.Collections.Generic;
using HollywoodStars.Models;
using System.Data.SqlClient;
using System;

namespace HollywoodStars.Data
{
    public static class MoviesData
    {
        public static List<Movie> GetList(string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                var result = new List<Movie>();

                using (var cmd = new SqlCommand("SELECT * FROM Movies", cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var item = new Movie
                            {
                                MovieId = Convert.ToInt32(dr["MovieId"]),
                                Title = Convert.ToString(dr["Title"]),
                                ReleaseYear = Convert.ToInt32(dr["ReleaseYear"])
                            };
                            result.Add(item);
                        }
                    }
                }

                return result;
            }
        }

        public static Movie GetMovie(int id, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                Movie result = null;

                using (var cmd = new SqlCommand("SELECT * FROM Movies WHERE MovieId = @MovieId", cn))
                {
                    cmd.Parameters.AddWithValue("@MovieId", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // If a record is found
                        {
                            result = new Movie
                            {
                                MovieId = Convert.ToInt32(dr["MovieId"]),
                                Title = Convert.ToString(dr["Title"]),
                                ReleaseYear = Convert.ToInt32(dr["ReleaseYear"])
                            };
                        }
                    }
                }

                return result;  // Return the result, which will be null if no movie is found
            }
        }

        public static void Insert(Movie movie, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("INSERT INTO Movies (Title, ReleaseYear) VALUES (@Title, @ReleaseYear)", cn))
                {
                    cmd.Parameters.AddWithValue("@Title", movie.Title);
                    cmd.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(Movie movie, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("UPDATE Movies SET Title = @Title, ReleaseYear = @ReleaseYear WHERE MovieId = @MovieId", cn))
                {
                    cmd.Parameters.AddWithValue("@Title", movie.Title);
                    cmd.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@MovieId", movie.MovieId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("DELETE FROM Movies WHERE MovieId = @MovieId", cn))
                {
                    cmd.Parameters.AddWithValue("@MovieId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool HasActors(Movie movie, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM ActorMovies WHERE MovieId = @MovieId", cn))
                {
                    cmd.Parameters.AddWithValue("@MovieId", movie.MovieId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}
