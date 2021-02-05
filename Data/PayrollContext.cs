using System;
using Microsoft.EntityFrameworkCore;
using PayrollApi.Models;

namespace PayrollApi.Data
{
    public class PayrollContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=Payrolls;User ID=sa;Password=Jorker2152029;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(x => x.Subjects)
                .WithMany(x => x.Students);
        }
    }
}
