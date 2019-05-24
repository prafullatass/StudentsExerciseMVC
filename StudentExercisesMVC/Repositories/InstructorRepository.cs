using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Repositories
{
    public class InstructorRepository
    {
        private static IConfiguration _config;

        public static void SetConfig(IConfiguration config)
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

        public static List<Instructor> GetAllInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();
            /*using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.Id,        
                                    i.FirstName,
                                    i.LastName, 
                                    i.SlackHandle,
                                    i.Specality,
                                    c.Id CohortId,
                                    c.CohortName
                                    FROM Instructor i JOIN Cohort c ON i.CohortId = c.Id
                                ";

                    SqlDataReader reader = cmd.ExecuteReader();*/
                    string str = @"SELECT i.Id,        
                                    i.FirstName,
                                    i.LastName, 
                                    i.SlackHandle,
                                    i.Specality,
                                    c.Id CohortId,
                                    c.CohortName
                                    FROM Instructor i JOIN Cohort c ON i.CohortId = c.Id
                                ";

                    DataSet reader = Main.ExecuteWithReader(str, null);
            DataTable dt= reader.Tables[0];
            foreach(DataRow dr in dt.Rows)
            {
                str = dr["LastName"].ToString();
                instructors.Add(new Instructor
                {
                    Id = int.Parse(dr["Id"].ToString()),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    SlackHandle = dr["SlackHandle"].ToString(),
                    Specality = dr["Specality"].ToString(),
                    CohortId = int.Parse(dr["CohortId"].ToString()),
                    Cohort = new Cohort
                    {
                        Id = int.Parse(dr["CohortId"].ToString()),
                        CohortName = dr["CohortName"].ToString(),
                    }
                });
            }
            return instructors;
                    /* while (reader.Read())
                    {
                        instructors.Add(new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            Specality = reader.GetString(reader.GetOrdinal("Specality")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                CohortName = reader.GetString(reader.GetOrdinal("CohortName")),
                            }
                        });
                    }
                    reader.Close(); */
        }


        public static Instructor GetSingleInstructor(int id)
        {
            Instructor instructor = null;
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT i.Id,        
                                    i.FirstName,
                                    i.LastName, 
                                    i.SlackHandle,
                                    i.Specality,
                                    c.Id CohortId,
                                    c.CohortName
                                    FROM Instructor i JOIN Cohort c ON i.CohortId = c.Id
                                    WHERE i.id = @id
                                ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            Specality = reader.GetString(reader.GetOrdinal("Specality")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
                            Cohort = new Cohort
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                CohortName = reader.GetString(reader.GetOrdinal("CohortName")),
                            }
                        };
                    }
                    reader.Close();
                }
            }
            return instructor;
        }


        public static void UpdateInstructor (Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Student SET FirstName = @firstName,
                                                        LastName =  @lastName, 
                                                        SlackHandle = @handle, 
                                                        Specality = @Specality,
                                                        CohortId = @cId         
                                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@firstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Specality", instructor.Specality));
                    cmd.Parameters.Add(new SqlParameter("@handle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cId", instructor.CohortId));
                    cmd.Parameters.Add(new SqlParameter("@Id", instructor.Id));

                    cmd.ExecuteNonQuery();
                }
            }

        }
        public static int InsertInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Instructor 
                                        (FirstName, LastName, SlackHandle, CohortId, Specality)         
                                         OUTPUT INSERTED.Id                                                       
                                         VALUES (@firstName, @lastName, @handle, @cId, @Specality)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Specality", instructor.Specality));
                    cmd.Parameters.Add(new SqlParameter("@handle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cId", instructor.CohortId));
                    cmd.Parameters.Add(new SqlParameter("@Id", instructor.Id));

                    return ((int)cmd.ExecuteScalar());
                }
            }
        }

        public static void DeleteInstructor(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Instructor 
                                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
