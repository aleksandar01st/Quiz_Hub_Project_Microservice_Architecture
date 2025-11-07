using Microsoft.EntityFrameworkCore;
using ResultsService.Models;

namespace ResultsService.Data
{
    public class ResultsServiceContext : DbContext
    {
        public ResultsServiceContext(DbContextOptions<ResultsServiceContext> options) : base(options) { }

        public DbSet<UserQuizResult> UserQuizResults { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserQuizResult>()
                .HasMany(r => r.UserAnswers)
                .WithOne(a => a.Result)
                .HasForeignKey(a => a.ResultId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
