using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IS_460_Assignment_2_Andrew_Horton.Models
{
    public class Student
    {

        [Required(ErrorMessage = "StudentID is required")]
        [RegularExpression (@"N\d{8}")]
        public string StudentID { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(25, ErrorMessage = "LastName must be less than 25 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(20, ErrorMessage = "FirstName must be less than 20 characters")]
        public string FirstName { get; set; }


        public int? CreditsEarned { get; set; }

        public double? GPA { get; set; }     

        public string Level { get; set; }
        public string[] LevelSelectTagHelpers = new[] { "Freshman", "Sophomore", "Junior", "Senior" };

        public bool Graduated { get; set; }


    }
}
