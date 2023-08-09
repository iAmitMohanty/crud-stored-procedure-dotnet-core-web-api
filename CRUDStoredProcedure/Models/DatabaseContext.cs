using Microsoft.EntityFrameworkCore;

namespace CRUDStoredProcedure.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<UserInfo>? UserInfo { get; set; }
    }
}
