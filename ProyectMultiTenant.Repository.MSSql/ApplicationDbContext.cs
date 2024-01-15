using Microsoft.EntityFrameworkCore;
using ProyectMultiTenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectMultiTenant.Repository.MSSql
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Organization> Organization { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Token> Token { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=PCET-LLAURENCIO\\SQLEXPRESS2019;Initial Catalog=MultitenantDB;Persist Security Info=True;User ID=sa;Password=123456789;")
        //        .EnableSensitiveDataLogging(true);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ParametroConfig(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void ParametroConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>(e => {
                e
                .ToTable("Organization")
                .HasKey(x => x.Id);
                e.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(e => {
                e
                .ToTable("Users")
                .HasKey(x => x.Id);
                e.Property(p => p.Id)
                .ValueGeneratedOnAdd();

                e
                .HasOne(p => p.Organization)
                .WithMany()
                .HasForeignKey(f => f.IdOrganization);
            });

            modelBuilder.Entity<Token>(e => {
                e
                .ToTable("Token")
                .HasKey(x => x.Id);
                e.Property(p => p.Id)
                .ValueGeneratedOnAdd();

                e
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(f => f.IdUser);
            });
        }
    }
}
