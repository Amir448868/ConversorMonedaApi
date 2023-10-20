﻿using ConversorMonedaApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ConversorMonedaApi.Data
{
    public class ConversorContext : DbContext
    {
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<ResquestLog> RequestsLog { get; set; }

        public ConversorContext(DbContextOptions<ConversorContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.RequestLogs)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

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
