using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Users { get; set; }         
    }
}
