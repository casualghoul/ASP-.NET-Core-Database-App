using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IS_460_Assignment_3_Andrew_Horton.Models
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "StudentID is required")]
        [RegularExpression (@"N\d{8}", ErrorMessage = "StudentID must match the pattern Nxxxxxxxx")]
        [Display (Name ="Student ID", Prompt ="Enter a Student ID: Nxxxxxxxx", Description = "Unique Student Identifier")]

        public string StudentID { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(25, ErrorMessage = "LastName must be less than 25 characters")]
        [Display(Name = "Last Name", Prompt = "Enter a Last Name", Description = "Student Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(20, ErrorMessage = "FirstName must be less than 20 characters")]
        [Display(Name = "First Name", Prompt = "Enter a First Name", Description = "Student First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Credits Earned", Prompt = "Enter Credits Earned", Description = "Credits Earned by a Student")]
        public int? CreditsEarned { get; set; }

        [Display(Name = "GPA", Prompt = "Enter a GPA", Description = "Student Grade Point Average")]
        public double? GPA { get; set; }

        [Display(Name = "Level", Prompt = "Enter a Level", Description = "Student Level")]
        public string Level { get; set; }

        public string[] LevelSelectTagHelpers = new[] { "Freshman", "Sophomore", "Junior", "Senior" };

        [Display(Name = "Graduated", Prompt = "Signify whether the student has graduated or not", Description = "Student Graduation Status")]
        public bool Graduated { get; set; }


    }
}
