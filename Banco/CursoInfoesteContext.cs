using CursoInfoeste.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CursoInfoeste.Models.Base;
using CursoInfoeste.Services;
using Microsoft.AspNetCore.Identity;

namespace CursoInfoeste.Banco
{
    public class CursoInfoesteContext(DbContextOptions<CursoInfoesteContext> options, Persistencia persistencia) : DbContext(options)
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            ApplyTenantTypes(modelBuilder);

            modelBuilder.Entity<Tenant>(x =>
            {

            });

            modelBuilder.Entity<CashRegister>(entity =>
            {
                entity.HasIndex(x => new{ x.TenantId, x.Number });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(x => x.TenantId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(x => x.TenantId);
            });
        }

        private void ApplyTenantTypes(ModelBuilder modelBuilder)
        {
            var types = modelBuilder.Model.GetEntityTypes();
            Expression<Func<int>> tenantId = () => persistencia.TenantId;
            var right = tenantId.Body;
            foreach (var item in types)
            {
                if (item.ClrType != typeof(BaseTenantEntity)) continue;
                
                // Adiciona QueryFilter
                var tenantIdProperty = item.FindProperty("TenantId");
                var parameter = Expression.Parameter(item.ClrType, "p");
                var left = Expression.Property(parameter, tenantIdProperty!.PropertyInfo!);
                var filter = Expression.Lambda(Expression.Equal(left,right), parameter);
                modelBuilder.Entity(item.ClrType).HasQueryFilter(filter);
            }
        }
    }
}
