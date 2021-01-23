using Microsoft.EntityFrameworkCore;

namespace QuasarFireOperation.Persistence.Sql.Context
{
    public class QuasarFireOperationContext : DbContext
    {
        public QuasarFireOperationContext()
        {
        }

        public QuasarFireOperationContext(DbContextOptions<QuasarFireOperationContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}