using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Tenant;

namespace ProyectMultiTenant.Repository.MSSql
{
    public class MultiTenantDbContext : DbContext
    {
        private IConfiguration configuration;
        private ITenantContext tenant;
        public DbSet<Product> Product { get; set; }
        public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options,
                                    IConfiguration configuration,
                                    ITenantContext  tenant) : base(options)
        {
            this.configuration = configuration;
            this.tenant = tenant;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStr = configuration.GetConnectionString("ConnectionStringSqlServerTenant2").Replace("Tenant", tenant.CurrentTenant.Name);
            optionsBuilder.UseSqlServer(connectionStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ParametroConfig(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void ParametroConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e => {
                e
                .ToTable("Product")
                .HasKey(x => x.Id);
                e.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });
        }
    }
}
