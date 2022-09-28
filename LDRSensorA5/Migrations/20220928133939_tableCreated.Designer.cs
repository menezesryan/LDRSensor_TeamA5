﻿// <auto-generated />
using System;
using LDRSensorA5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LDRSensorA5.Migrations
{
    [DbContext(typeof(LdrDBContext))]
    [Migration("20220928133939_tableCreated")]
    partial class tableCreated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("LDRSensorA5.Models.LDRData", b =>
                {
                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<float>("Current")
                        .HasColumnType("REAL");

                    b.Property<float>("Lux")
                        .HasColumnType("REAL");

                    b.HasKey("TimeStamp");

                    b.ToTable("LDRData");
                });
#pragma warning restore 612, 618
        }
    }
}
