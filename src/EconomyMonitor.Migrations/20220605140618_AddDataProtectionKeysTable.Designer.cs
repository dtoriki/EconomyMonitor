﻿// <auto-generated />
using System;
using EconomyMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    [DbContext(typeof(AppRepository))]
    [Migration("20220605140618_AddDataProtectionKeysTable")]
    partial class AddDataProtectionKeysTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-preview.4.22229.2");

            modelBuilder.Entity("EconomyMonitor.Data.Entities.DatePeriodEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("EndingDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Income")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("StartingDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DatePeriods");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FriendlyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Xml")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });
#pragma warning restore 612, 618
        }
    }
}
