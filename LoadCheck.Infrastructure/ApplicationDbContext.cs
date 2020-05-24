using System.Data.Entity;
using LoadCheck.Core.Entities;

namespace LoadCheck.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=LoadCheck;Integrated Security=SSPI;")
        {
        }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<SiteRoot> SiteRoots { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>()
                .HasRequired(t => t.SiteRoot)
                .WithMany(r => r.Tests)
                .HasForeignKey(t => t.SiteRootId);

            modelBuilder.Entity<TestResult>()
                .HasRequired(tr => tr.Test)
                .WithMany(t => t.TestResults)
                .HasForeignKey(tr => tr.TestId);

        }
    }
}
