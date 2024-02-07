using ConversorMonedaApi.Data.Models.Enum;
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

        public DbSet<Subscriptions> Subscriptions { get; set; }

        public ConversorContext(DbContextOptions<ConversorContext> options) : base(options)
        { }
        //en onmodelcreating se establecen las relaciones entre las tablas y se insertan los datos iniciales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Subscriptions Free = new Subscriptions {RequestId=1,TypeUser = "free", Value = 10 };
            Subscriptions Trial = new Subscriptions {RequestId = 2, TypeUser = "trial", Value = 100 };
            Subscriptions Premium = new Subscriptions { RequestId = 3, TypeUser = "premium", Value = -1};
            modelBuilder.Entity<Subscriptions>().HasData(Free, Trial, Premium);

            User userAdmin = new User { 
                UserId = 1, 
                UserName = "string",
                Password = "string",
                TypeUser = "premium",
                ConversionCounter = 0, 
                Role = Role.Admin 
            };


            modelBuilder.Entity<User>()
                .HasMany(u => u.Conversions)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
            //un usuario puede tener muchas conversiones

             modelBuilder.Entity<Conversion>()
                .HasOne(c => c.CurrencyFrom)
                .WithMany()
                .HasForeignKey(c => c.CurrencyFromId);
            //una conversion tiene una moneda de origen

            modelBuilder.Entity<Conversion>()
                .HasOne(c => c.CurrencyTo)
                .WithMany()
                .HasForeignKey(c => c.CurrencyToId);
            //una conversion tiene una moneda de destino

            base.OnModelCreating(modelBuilder);
        }
    }
}
