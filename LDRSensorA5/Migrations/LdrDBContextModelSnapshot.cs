﻿// <auto-generated />
using System;
using LDRSensorA5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LDRSensorA5.Migrations
{
    [DbContext(typeof(LdrDBContext))]
    partial class LdrDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("LDRSensorA5.Models.LDRData", b =>
                {
                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<double>("Current")
                        .HasColumnType("REAL");

                    b.Property<double>("Lux")
                        .HasColumnType("REAL");

                    b.HasKey("TimeStamp");

                    b.ToTable("LDRData");
                });
#pragma warning restore 612, 618
        }
    }
}
