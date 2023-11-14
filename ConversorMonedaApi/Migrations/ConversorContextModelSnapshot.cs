﻿// <auto-generated />
using ConversorMonedaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConversorMonedaApi.Migrations
{
    [DbContext(typeof(ConversorContext))]
    partial class ConversorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("ConversorMonedaApi.Entities.Conversion", b =>
                {
                    b.Property<int>("ConversionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyFromId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyToId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Result")
                        .HasColumnType("REAL");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConversionId");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.HasIndex("UserId");

                    b.ToTable("Conversions");
                });

            modelBuilder.Entity("ConversorMonedaApi.Entities.Currency", b =>
                {
                    b.Property<int>("MonedaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("MonedaId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("ConversorMonedaApi.Entities.Subscriptions", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeUser")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("RequestId");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            RequestId = 1,
                            TypeUser = "free",
                            Value = 10
                        },
                        new
                        {
                            RequestId = 2,
                            TypeUser = "trial",
                            Value = 100
                        },
                        new
                        {
                            RequestId = 3,
                            TypeUser = "premium",
                            Value = -1
                        });
                });

            modelBuilder.Entity("ConversorMonedaApi.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConversionCounter")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ConversorMonedaApi.Entities.Conversion", b =>
                {
                    b.HasOne("ConversorMonedaApi.Entities.Currency", "CurrencyFrom")
                        .WithMany()
                        .HasForeignKey("CurrencyFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConversorMonedaApi.Entities.Currency", "CurrencyTo")
                        .WithMany()
                        .HasForeignKey("CurrencyToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConversorMonedaApi.Entities.User", "User")
                        .WithMany("Conversions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrencyFrom");

                    b.Navigation("CurrencyTo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConversorMonedaApi.Entities.User", b =>
                {
                    b.Navigation("Conversions");
                });
#pragma warning restore 612, 618
        }
    }
}
