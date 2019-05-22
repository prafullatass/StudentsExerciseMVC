using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Instructor
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = " Slack Handle")]
        public string SlackHandle { get; set; }

        [Required]
        public Cohort Cohort { get; set; }

        [Required]
        [StringLength(80)]
        public string Specality { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} instructing {Cohort.CohortName}, has spcality in {Specality} and handle slack {SlackHandle}";
        }
    }
}
