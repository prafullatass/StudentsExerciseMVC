using StudentExercisesMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class AssignExercises
    {
        public Student student { get; set; }
        public List<Exercise> AssignedExercises = new List<Exercise>();
        
        public AssignExercises(Student stud)
        {
            student = stud;
            AssignedExercises =  StudentsRepository.getStudentExercises(stud.Id);
        }
    }
}
