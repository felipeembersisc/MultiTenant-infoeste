using CursoInfoeste.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CursoInfoeste.Services;
using Microsoft.AspNetCore.Identity;

namespace CursoInfoeste.Banco
{
    public class CursoInfoesteContext(DbContextOptions<CursoInfoesteContext> options, Persistencia persistencia) : DbContext(options)
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(x =>
            {

            });

            modelBuilder.Entity<CashRegister>(entity =>
            {
                entity.HasIndex(x => new{ x.TenantId, x.Number });
                entity.HasQueryFilter(x => x.TenantId == persistencia.TenantId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(x => x.TenantId);
            });
        }
    }
}
