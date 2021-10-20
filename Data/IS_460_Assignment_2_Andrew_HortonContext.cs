using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    }
}
