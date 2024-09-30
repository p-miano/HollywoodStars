using HollywoodStars.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;

namespace HollywoodStars.Data
{
    public static class ActorsData
    {
        public static List<Actor> GetList(string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                var result = new List<Actor>();

                using (var cmd = new SqlCommand("SELECT * FROM Actors", cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var item = new Actor();
                            item.ActorId = Convert.ToInt32(dr["ActorId"]);
                            item.FullName = Convert.ToString(dr["FullName"]);
                            result.Add(item);
                        }
                    }
                }

                return result;
            }
        }

        public static Actor GetActor(int id, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                Actor result = null;

                using (var cmd = new SqlCommand("SELECT * FROM Actors WHERE ActorId = @ActorId", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // If a record is found
                        {
                            result = new Actor
                            {
                                ActorId = Convert.ToInt32(dr["ActorId"]),
                                FullName = Convert.ToString(dr["FullName"])
                            };
                        }
                    }
                }
                return result;  // Return the result, which will be null if no actor is found
            }
        }

        public static void Insert(Actor actor, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("INSERT INTO Actors (FullName) VALUES (@FullName)", cn))
                {
                    cmd.Parameters.AddWithValue("@FullName", actor.FullName);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void Update(Actor actor, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("UPDATE Actors SET FullName = @FullName WHERE ActorId = @ActorId", cn))
                {
                    cmd.Parameters.AddWithValue("@FullName", actor.FullName);
                    cmd.Parameters.AddWithValue("@ActorId", actor.ActorId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("DELETE FROM Actors WHERE ActorId = @ActorId", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool HasMovies(Actor actor, string conn)
        {
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM ActorMovies WHERE ActorId = @ActorId", cn))
                {
                    cmd.Parameters.AddWithValue("@ActorId", actor.ActorId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }

            }
        }
    }
}
