using Microsoft.AspNetCore.Mvc.Rendering;
using StudentExercisesMVC.Repositories;
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

        public IntructorCreateNewModel()
        {
            MakeCohortSelectList();
        }
        public IntructorCreateNewModel(Instructor inst)
        {
            MakeCohortSelectList();
            Instructor = inst;
        }

        public void MakeCohortSelectList()
        {
            List<Cohort> cohorts = CohortRepository.GetAllCohorts();
            Cohorts = cohorts.Select(li => new SelectListItem
            {
                Text = li.CohortName,
                Value = li.Id.ToString()
            }).ToList();
        }
    }
}
