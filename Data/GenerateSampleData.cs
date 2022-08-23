using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_460_Assignment_3_Andrew_Horton.Models;
using IS_460_Assignment_3_Andrew_Horton.Data;

namespace IS_460_Assignment_3_Andrew_Horton.Data
{
    public class GenerateSampleData
    {
        public static void Initialize(IS_460_Assignment_3_Andrew_HortonContext context)
        {
            context.Database.EnsureCreated();
            if (context.Student.Any())
            {
                // return;
            }

            var students = new Student[]
            {
                new Student{StudentID="N32345678", FirstName="Andrew", LastName="Horton", CreditsEarned=10, GPA=3.4, Level="Freshman", Graduated=false},
                new Student{StudentID="N43456789", FirstName="Johnny", LastName="Appleseed", CreditsEarned=20, GPA=3.0, Level="Sophomore", Graduated=false},
                new Student{StudentID="N53456789", FirstName="Sarah", LastName="Smith", CreditsEarned=30, GPA=3.8, Level="Junior", Graduated=false},
                new Student{StudentID="N63456789", FirstName="Jessica", LastName="Johnson", CreditsEarned=120, GPA=4.0, Level="Senior", Graduated=true},
                new Student{StudentID="N73456789", FirstName="Todd", LastName="Howard", CreditsEarned=95, GPA=3.9, Level="Junior", Graduated=false},
                new Student{StudentID="N83456789", FirstName="Mike", LastName="Jones", CreditsEarned=65, GPA=2.5, Level="Junior", Graduated=false},
                new Student{StudentID="N93456789", FirstName="Bob", LastName="Joe", CreditsEarned=120, GPA=3.5, Level="Junior", Graduated=true},
                new Student{StudentID="N45678912", FirstName="Hank", LastName="Toms", CreditsEarned=54, GPA=3.1, Level="Sohpomore", Graduated=false},
                new Student{StudentID="N55678912", FirstName="Chuck", LastName="Gold", CreditsEarned=24, GPA=2.5, Level="Freshman", Graduated=true}
            };

            foreach (Student s in students)
            {
                context.Student.Add(s);
            }
            context.SaveChanges();
        }
    }
}
        

