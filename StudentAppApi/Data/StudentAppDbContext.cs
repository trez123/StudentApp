using StudentAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentAppApi.Data
{
    public class StudentAppDbContext : DbContext
    {
        public StudentAppDbContext(DbContextOptions<StudentAppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
