﻿// <auto-generated />
using System;
using MASA.Framework.Admin.Service.User.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MASA.Framework.Admin.Service.User.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
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

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("TimesSent")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "State", "ModificationTime" }, "index_state_modificationtime");

                    b.HasIndex(new[] { "State", "TimesSent", "ModificationTime" }, "index_state_timessent_modificationtime");

                    b.ToTable("IntegrationEventLog", (string)null);
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_time");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<string>("Describtion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("describtion");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("modifier_time");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("modifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("parent_id");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.ToTable("department", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.DepartmentUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("department_users", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.User", b =>
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

                    b.Property<bool>("Enable")
                        .HasColumnType("bit")
                        .HasColumnName("enable");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("modifier_time")
                        .HasDefaultValueSql("SYSDATETIME()");

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

                    b.HasKey("Id");

                    b.ToTable("users", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_time");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creator");

                    b.Property<string>("Describtion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("describtion");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("modifier_time");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("modifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.ToTable("user_groups", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserGroupItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("UserGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.ToTable("user_group_items", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("user_roles", "user");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.DepartmentUser", b =>
                {
                    b.HasOne("MASA.Framework.Admin.Service.User.Domain.Aggregates.Department", "Department")
                        .WithMany("DepartmentUsers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserGroupItem", b =>
                {
                    b.HasOne("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserGroup", "UserGroup")
                        .WithMany("UserGroupItems")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserRole", b =>
                {
                    b.HasOne("MASA.Framework.Admin.Service.User.Domain.Aggregates.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.Department", b =>
                {
                    b.Navigation("DepartmentUsers");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("MASA.Framework.Admin.Service.User.Domain.Aggregates.UserGroup", b =>
                {
                    b.Navigation("UserGroupItems");
                });
#pragma warning restore 612, 618
        }
    }
}
