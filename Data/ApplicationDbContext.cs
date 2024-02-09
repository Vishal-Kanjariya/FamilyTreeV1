using FamilyTreeV1.Model;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeV1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
