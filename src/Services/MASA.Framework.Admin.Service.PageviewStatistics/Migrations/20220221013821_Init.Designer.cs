﻿// <auto-generated />
using System;
using MASA.Framework.Admin.Service.Logging.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MASA.Framework.Admin.Service.PageviewStatistics.Migrations
{
    [DbContext(typeof(PageviewStatisticsDbContext))]
    [Migration("20220221013821_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MASA.Framework.Admin.Contracts.Logging.OperationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClientIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("operation_log");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Contracts.PageviewStatistics.PageviewDayStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IPCount")
                        .HasColumnType("int");

                    b.Property<int>("PV")
                        .HasColumnType("int");

                    b.Property<int>("UV")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("pageview_day_statistics");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Contracts.PageviewStatistics.PageviewHourStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IPCount")
                        .HasColumnType("int");

                    b.Property<int>("PV")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("UV")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("pageview_hour_statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
