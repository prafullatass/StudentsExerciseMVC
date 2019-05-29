using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Repositories
{
    public class StudentsRepository
    {
        private static IConfiguration _config;

        public static void setConfig(IConfiguration config)
        {
            _config = config;
        }

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public static Student getSingleStudent(int id)
        {
            Student student = null;
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.Id,
                                        s.FirstName,
                                        s.LastName,
                                        s.SlackHandle,
                                        s.CohortId
                                    FROM Student s
                                    WHERE s.Id = @studentId";
                    cmd.Parameters.Add(new SqlParameter("@studentId", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };
                    }
                }
            }
            return student;
        }

        public static List<Exercise> getStudentExercises(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT se.StudentId, 
                                            se.ExerciseId,
                                            e.Language,
                                            e.Title
                                        FROM StudentExercise se
                                        JOIN Exercise e on se.ExerciseId = e.Id
                                        WHERE studentId = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader =  cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                            Language = reader.GetString(reader.GetOrdinal("Language")),
                            Title = reader.GetString(reader.GetOrdinal("Title"))
                        });
                    }
                    reader.Close();
                    return exercises;
                }
            }
        }
    }
}
