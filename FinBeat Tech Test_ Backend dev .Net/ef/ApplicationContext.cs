using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace FinBeat_Tech_Test__Backend_dev_.Net.ef
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CodeValueClass> CodeValueDbSet => Set<CodeValueClass>();
        public DbSet<DBLoggerClass> DBLoggerDbSet => Set<DBLoggerClass>();
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CodeValueClass>()
            .Property(e => e.row_id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<CodeValueClass>()
            .HasKey(t => t.row_id);

            modelBuilder.Entity<DBLoggerClass>()
            .Property(e => e.row_id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<DBLoggerClass>()
            .HasKey(t => t.row_id);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=appdb.db");
        }
    }
}
