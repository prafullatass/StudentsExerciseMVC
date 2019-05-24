using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class IntructorCreateNewModel
    {
        public Instructor Instructor { get; set; }
        public List<SelectListItem> Cohorts = new List<SelectListItem>();
        public void getAllCohorts(SqlConnection conn)
        {
            using (conn)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, CohortName FROM Cohort";
                    SqlDataReader reader =  cmd.ExecuteReader();
                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        cohorts.Add(new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CohortName = reader.GetString(reader.GetOrdinal("CohortName"))
                        });
                    }
                    reader.Close();
                    cohorts.Select(li => new SelectListItem
                    {
                        Text = li.CohortName,
                        Value = li.Id.ToString()
                    }).ToList();
                }
            }
        }
    }
}
