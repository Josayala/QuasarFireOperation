using Microsoft.EntityFrameworkCore;

namespace QuasarFireOperation.Persistence.Sql.Context
{
    public class PersistentContext : QuasarFireOperationContext
    {
        public PersistentContext(DbContextOptions<QuasarFireOperationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define "missing" FKs so the linq queries are plausible
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
        }
    }
}