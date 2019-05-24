using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class InstrctorViewAllModel
    {
        public Student Student { get; set; } = new Student();

        public string CohortName { get; set; }
        public object Cohorts { get; private set; }

        public InstrctorViewAllModel() { }

        public InstrctorViewAllModel(SqlConnection connection)
        {
            GetAllInstrucorWithCohortName(connection);
        }

        public void GetAllInstrucorWithCohortName(SqlConnection conn)
        {
            using (conn)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.Id,
                                        i.FirstName,
                                        i.LastName,
                                        i.SlackHandle,
                                        i.CohortId,
                                        c.CohortName
                                    FROM Instructor i JOIN Cohort c
                                    ON i.CohortId = c.CohortId";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        };
                        instructors.Add(instructor);
                        CohortName = reader.GetString(reader.GetOrdinal("CohortName"));
                    }

                    /*Cohorts = cohorts.Select(li => new SelectListItem
                    {
                        Text = li.CohortName,
                        Value = li.Id.ToString()
                    }).ToList();

                    Cohorts.Insert(0, new SelectListItem
                    {
                        Text = "Choose cohort...",
                        Value = "0"
                    });
                    reader.Close();*/
                }
            }
        }
    }
}
