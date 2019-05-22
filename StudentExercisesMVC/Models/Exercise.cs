using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Exercise
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Title { get; set; }

        [Required]
        [StringLength(80)]
        public string Language { get; set; }

        public override string ToString()
        {
            return $"{Id} : {Title} ({Language})";
        }
    }
}
