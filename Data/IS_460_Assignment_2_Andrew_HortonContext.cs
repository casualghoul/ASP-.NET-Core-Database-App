using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Needed for DbContext (you may need to install via Project > Manage NuGet Packages...
using Microsoft.EntityFrameworkCore;
// Needed for IServiceProvider.GetRequiredService.
using Microsoft.Extensions.DependencyInjection;
using IS_460_Assignment_2_Andrew_Horton.Models;

namespace IS_460_Assignment_2_Andrew_Horton.Data
{
    public class IS_460_Assignment_2_Andrew_HortonContext : DbContext
    {
        public IS_460_Assignment_2_Andrew_HortonContext (DbContextOptions<IS_460_Assignment_2_Andrew_HortonContext> options)
            : base(options)
        {
        }

        public DbSet<IS_460_Assignment_2_Andrew_Horton.Models.Student> Student { get; set; }

        public class CreateSampleData
        {
            public static void Initialize(System.IServiceProvider serviceProvider)
            {
                var context = new
                    IS_460_Assignment_2_Andrew_HortonContext(serviceProvider.GetRequiredService<
                        DbContextOptions<IS_460_Assignment_2_Andrew_Horton.Data.IS_460_Assignment_2_Andrew_HortonContext>>());

                Models.Student s = new Models.Student();
                s.StudentID = "N12345678";
                s.LastName = "Horton";
                s.FirstName = "Andrew";
                s.CreditsEarned = 120;
                s.GPA = 4.0;
                s.Level = "Senior";
                s.Graduated = true;
                context.Add(s);

                context.SaveChanges();

            }

        }


        
    }
}
