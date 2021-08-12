using App.Domain.Entities;
using App.Infrastructure.Mappings;
using App.Shared;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Contexts
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext() { }

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Runtime.ConnectionString);
            options.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProviderMap());
        }

    }
}
