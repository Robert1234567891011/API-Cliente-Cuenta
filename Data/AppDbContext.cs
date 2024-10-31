using BCP.Models;
using Microsoft.EntityFrameworkCore;

namespace BCP.Data
{
    //autor Richard Robertopoma/github.com/Robert1234567891011
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .Property(c => c.Monto)
                .HasColumnType("decimal(18,2)");
        }
    }
}
