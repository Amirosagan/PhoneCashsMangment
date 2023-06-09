﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VodafoneCashApi.Data;

#nullable disable

namespace VodafoneCashApi.Migrations
{
    [DbContext(typeof(data))]
    [Migration("20230510005654_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("VodafoneCashApi.Models.Numbers", b =>
                {
                    b.Property<string>("Number")
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.HasKey("Number");

                    b.ToTable("Numbers");
                });

            modelBuilder.Entity("VodafoneCashApi.Models.Transactions", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new Guid("78c14eda-efa0-41d9-a2ff-8c814d74aeb8"));

                    b.Property<decimal>("CashAfter")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CashBefore")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(2023, 5, 10, 3, 56, 54, 136, DateTimeKind.Local).AddTicks(2235));

                    b.Property<string>("NumberId")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionId")
                        .HasName("PK_Transactions");

                    b.HasIndex("NumberId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("VodafoneCashApi.Models.Transactions", b =>
                {
                    b.HasOne("VodafoneCashApi.Models.Numbers", null)
                        .WithMany("Transactions")
                        .HasForeignKey("NumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VodafoneCashApi.Models.Numbers", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
