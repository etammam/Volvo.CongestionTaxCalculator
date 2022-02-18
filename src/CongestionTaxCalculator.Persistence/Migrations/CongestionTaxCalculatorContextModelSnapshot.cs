﻿// <auto-generated />
using System;
using CongestionTaxCalculator.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CongestionTaxCalculator.Persistence.Migrations
{
    [DbContext(typeof(CongestionTaxCalculatorContext))]
    partial class CongestionTaxCalculatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("CongestionTaxCalculator.Domain.City", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Code")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            Code = 1,
                            Name = "Gothenburg"
                        });
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.Ignore", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("DaysBeforeHoliday")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Month")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ignores");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            DaysBeforeHoliday = 1,
                            Month = "[\"July\"]"
                        });
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.Rate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CityId")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("End")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("RateValue")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("Start")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Rates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0d9fb9c8-1f74-44b2-b8ae-287235dc5af1"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(6, 29, 0),
                            RateValue = 8m,
                            Start = new TimeOnly(6, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("1c9c1143-0ad8-4f8e-b9c9-bdb65df92de5"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(6, 59, 0),
                            RateValue = 13m,
                            Start = new TimeOnly(6, 30, 0)
                        },
                        new
                        {
                            Id = new Guid("0842e0e6-c6d0-46a5-a3ec-294fe3a743bb"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(7, 59, 0),
                            RateValue = 18m,
                            Start = new TimeOnly(7, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("87b12225-9606-415e-a447-1067a6d37cac"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(8, 29, 0),
                            RateValue = 13m,
                            Start = new TimeOnly(8, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("12858360-07a6-4bea-9a51-0dcb42667577"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(14, 59, 0),
                            RateValue = 8m,
                            Start = new TimeOnly(8, 30, 0)
                        },
                        new
                        {
                            Id = new Guid("eb43daa0-9026-4821-92fd-8ee42dfbacf3"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(15, 29, 0),
                            RateValue = 13m,
                            Start = new TimeOnly(15, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("40369fa8-313b-4acf-99be-d3516f36f2b1"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(16, 59, 0),
                            RateValue = 18m,
                            Start = new TimeOnly(15, 30, 0)
                        },
                        new
                        {
                            Id = new Guid("02c23596-fda9-42e8-9a6e-40fe59a86a1d"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(17, 59, 0),
                            RateValue = 13m,
                            Start = new TimeOnly(17, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("b61927b8-cc15-4732-8d87-dda1f5d9c9cc"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(18, 29, 0),
                            RateValue = 8m,
                            Start = new TimeOnly(18, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("167926a7-4fd1-403a-a0c4-b053a3498dfe"),
                            CityId = new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"),
                            End = new TimeOnly(5, 59, 0),
                            RateValue = 0m,
                            Start = new TimeOnly(18, 30, 0)
                        });
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.TaxHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CityId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Issued")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TaxCost")
                        .HasColumnType("TEXT");

                    b.Property<string>("VehicleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("TaxHistories");
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.TaxPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CityId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Issued")
                        .HasColumnType("TEXT");

                    b.Property<string>("VehicleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("TaxPayments");
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.Ignore", b =>
                {
                    b.HasOne("CongestionTaxCalculator.Domain.City", "City")
                        .WithOne("Ignore")
                        .HasForeignKey("CongestionTaxCalculator.Domain.Ignore", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.Rate", b =>
                {
                    b.HasOne("CongestionTaxCalculator.Domain.City", "City")
                        .WithMany("Rates")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.TaxHistory", b =>
                {
                    b.HasOne("CongestionTaxCalculator.Domain.City", "City")
                        .WithMany("TaxHistories")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.TaxPayment", b =>
                {
                    b.HasOne("CongestionTaxCalculator.Domain.City", "City")
                        .WithMany("TaxPayments")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CongestionTaxCalculator.Domain.City", b =>
                {
                    b.Navigation("Ignore");

                    b.Navigation("Rates");

                    b.Navigation("TaxHistories");

                    b.Navigation("TaxPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
