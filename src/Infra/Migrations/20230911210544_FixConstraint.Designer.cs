﻿// <auto-generated />
using System;
using Infra.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230911210544_FixConstraint")]
    partial class FixConstraint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentUser", b =>
                {
                    b.Property<Guid>("DepartmentsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("DepartmentsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("DepartmentUser");
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .HasColumnType("text");

                    b.Property<byte[]>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("RolePart")
                        .HasColumnType("int");

                    b.Property<byte[]>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Sector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<byte[]>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Domain.Entities.Spent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<byte[]>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<Guid?>("SectorId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SectorId");

                    b.HasIndex("UserId");

                    b.ToTable("Spents");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DepartmentUser", b =>
                {
                    b.HasOne("Domain.Entities.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.HasOne("Domain.Entities.User", "Owner")
                        .WithOne("DepartmentOwner")
                        .HasForeignKey("Domain.Entities.Department", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Departments_Owner");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Sector", b =>
                {
                    b.HasOne("Domain.Entities.Department", "Department")
                        .WithMany("Sectors")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Domain.Entities.Spent", b =>
                {
                    b.HasOne("Domain.Entities.Sector", "Sector")
                        .WithMany("Spents")
                        .HasForeignKey("SectorId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Sector");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.Navigation("Sectors");
                });

            modelBuilder.Entity("Domain.Entities.Sector", b =>
                {
                    b.Navigation("Spents");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("DepartmentOwner")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
