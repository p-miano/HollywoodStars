using HollywoodStars.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace HollywoodStars.Data
{
    public static class ActorMoviesData
    {
        // Get the list of movies associated with a specific actor
        public static List<Movie> GetAssociatedMovieList(int actorId, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                var result = new List<Movie>();

                using (var cmd = new SqlCommand(@"SELECT m.MovieId, m.Title, m.ReleaseYear 
                                                  FROM Movies m
                                                  INNER JOIN ActorMovies am ON m.MovieId = am.MovieId
                                                  WHERE am.ActorId = @ActorId", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", actorId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var movie = new Movie
                            {
                                MovieId = Convert.ToInt32(dr["MovieId"]),
                                Title = Convert.ToString(dr["Title"]),
                                ReleaseYear = Convert.ToInt32(dr["ReleaseYear"])
                            };
                            result.Add(movie);
                        }
                    }
                }

                return result;
            }
        }

        // Get the list of movies NOT associated with a specific actor
        public static List<Movie> GetNonAssociatedMovieList(int actorId, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                var result = new List<Movie>();

                using (var cmd = new SqlCommand(@"SELECT m.MovieId, m.Title, m.ReleaseYear 
                                                  FROM Movies m
                                                  WHERE m.MovieId NOT IN (
                                                      SELECT am.MovieId 
                                                      FROM ActorMovies am 
                                                      WHERE am.ActorId = @ActorId)", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", actorId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var movie = new Movie
                            {
                                MovieId = Convert.ToInt32(dr["MovieId"]),
                                Title = Convert.ToString(dr["Title"]),
                                ReleaseYear = Convert.ToInt32(dr["ReleaseYear"])
                            };
                            result.Add(movie);
                        }
                    }
                }

                return result;
            }
        }

        // Insert a record into the ActorMovies join table (associate an actor with a movie)
        public static void Insert(int actorId, int movieId, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("INSERT INTO ActorMovies (ActorId, MovieId) VALUES (@ActorId, @MovieId)", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", actorId);
                    cmd.Parameters.AddWithValue("@MovieId", movieId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete a record from the ActorMovies join table (disassociate an actor from a movie)
        public static void Delete(int actorId, int movieId, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("DELETE FROM ActorMovies WHERE ActorId = @ActorId AND MovieId = @MovieId", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", actorId);
                    cmd.Parameters.AddWithValue("@MovieId", movieId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
