using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ukiyo.Api.WebApi.Identity
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<LoginToken> LoginTokens { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LoginToken>()
                .HasKey(e => e.Id);

            builder.Entity<LoginToken>()
                .Property(e => e.Id).ValueGeneratedNever();

            builder.Entity<LoginToken>()
                .HasOne(e => e.IdentityUser)
                .WithMany()
                .HasForeignKey(e => e.IdentityUserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}