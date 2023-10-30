using ConversorMonedaApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ConversorMonedaApi.Data
{
    public class ConversorContext : DbContext
    {
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<RemainingRequest> RemainingRequests { get; set; }

        public ConversorContext(DbContextOptions<ConversorContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RemainingRequest Free = new RemainingRequest {RequestId=1,TypeUser = "free", Value = 10 };
            RemainingRequest Trial = new RemainingRequest {RequestId = 2, TypeUser = "trial", Value = 100 };
            RemainingRequest Premium = new RemainingRequest { RequestId = 3, TypeUser = "premium", Value = 1000000000};
            modelBuilder.Entity<RemainingRequest>().HasData(Free, Trial, Premium);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Conversions)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

             modelBuilder.Entity<Conversion>()
                .HasOne(c => c.CurrencyFrom)
                .WithMany()
                .HasForeignKey(c => c.CurrencyFromId);

            modelBuilder.Entity<Conversion>()
                .HasOne(c => c.CurrencyTo)
                .WithMany()
                .HasForeignKey(c => c.CurrencyToId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
