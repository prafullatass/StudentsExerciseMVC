using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Slack Handle")]
        public string SlackHandle { get; set; }

        [Required]
        public int CohortId { get; set; }
        [Required]
        public Cohort Cohort { get; set; }
        public List<Exercise> Exercises { get; set; }

        public Student()
        {
            Exercises = new List<Exercise>();
        }
        public override string ToString()
        {
            string str = "";
            str = $"{Id}. {FirstName} {LastName} is in {Cohort.CohortName},and slack handle is {SlackHandle} \n";
            str += "-----------------Exercises------------------------------";
            Exercises.ForEach(ex => str += "\n      " + ex.ToString());
            return str;
        }
    }
}
