﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class StudentCreateViewModel
    {
        // A single student
        public Student Student { get; set; } = new Student();

        // All cohorts
        public List<SelectListItem> Cohorts = new List<SelectListItem>();

        public SqlConnection Connection;


        public StudentCreateViewModel() { }

        public StudentCreateViewModel(SqlConnection connection)
        {
            Connection = connection;
            GetAllCohorts();
        }

        public void GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.CohortName from Cohort c";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();

                    while (reader.Read())
                    {
                        Cohort cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CohortName = reader.GetString(reader.GetOrdinal("CohortName"))
                        };
                        cohorts.Add(cohort);
                    }

                    Cohorts = cohorts.Select(li => new SelectListItem
                    {
                        Text = li.CohortName,
                        Value = li.Id.ToString()
                    }).ToList();

                    Cohorts.Insert(0, new SelectListItem
                    {
                        Text = "Choose cohort...",
                        Value = "0"
                    });
                    reader.Close();
                }
            }
        }
    }
}
