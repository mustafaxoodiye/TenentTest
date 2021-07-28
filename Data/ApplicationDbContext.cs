using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly int _tenantId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, QueryStringTenantResolver tenantResolver)
            : base(options)
        {
            _tenantId = tenantResolver.GetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Global query filters.
            builder.Entity<Student>()
                .HasQueryFilter(s => s.TenantId == _tenantId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // TODO: Add the current tenant to the entities!
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var entry in entries)
            {
                if (entry.Entity is not MustHaveTenant<long> mustHaveTenantEntity)
                {
                    Console.WriteLine($"{entry.Entity} does not need tenant!");
                    continue;
                }

                Console.WriteLine($"Adding {_tenantId} tenant to {entry.Entity}!");
                entry.Property(nameof(mustHaveTenantEntity.CreatedAt)).CurrentValue = DateTimeOffset.UtcNow;
                entry.Property(nameof(mustHaveTenantEntity.TenantId)).CurrentValue = _tenantId;
            }

            // TODO: Add LastModifiedAt and LastModifiedBy for updating entries here.

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
