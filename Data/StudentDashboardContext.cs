using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models;

namespace StudentDashboard.Data
{
    public class StudentDashboardContext : DbContext
    {
        public StudentDashboardContext (DbContextOptions<StudentDashboardContext> options)
            : base(options)
        {
        }

        public DbSet<StudentDashboard.Models.student> student { get; set; } = default!;
        public DbSet<StudentDashboard.Models.Visits> Visits { get; set; } = default!;
    }
}
