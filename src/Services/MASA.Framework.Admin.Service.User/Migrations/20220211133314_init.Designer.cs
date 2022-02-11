﻿// <auto-generated />
using System;
using MASA.Framework.Admin.Service.User.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MASA.Framework.Admin.Service.User.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20220211133314_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MASA.BuildingBlocks.Dispatcher.IntegrationEvents.Logs.IntegrationEventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("TimesSent")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("integration_event_log", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregate.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("account");

                    b.Property<string>("Cover")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("cover");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_time");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit")
                        .HasColumnName("gender");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTimeOffset>("LastLoginTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("last_login_time");

                    b.Property<DateTimeOffset>("LastUpdateTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("last_update_time");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("modifier_time");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("modifier");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("salt");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.HasKey("Id");

                    b.ToTable("users", "user");
                });
#pragma warning restore 612, 618
        }
    }
}