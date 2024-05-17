using Microsoft.EntityFrameworkCore;
using SnakeA.UserModels.Users;
namespace SnakeA.Repository.Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

    }
}
