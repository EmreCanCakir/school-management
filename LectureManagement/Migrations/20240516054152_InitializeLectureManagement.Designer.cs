﻿// <auto-generated />
using System;
using LectureManagement.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LectureManagement.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240516054152_InitializeLectureManagement")]
    partial class InitializeLectureManagement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LectureManagement.Model.AcademicYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("EndDate")
                        .IsUnique();

                    b.HasIndex("StartDate")
                        .IsUnique();

                    b.ToTable("AcademicYears");
                });

            modelBuilder.Entity("LectureManagement.Model.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<float>("Credit")
                        .HasColumnType("real");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoursInWeek")
                        .HasColumnType("int");

                    b.Property<bool>("IsGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quota")
                        .HasColumnType("int");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("LectureManagement.Model.LectureInstructor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcademicYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("LectureId");

                    b.ToTable("LectureInstructors");
                });

            modelBuilder.Entity("LectureManagement.Model.LectureSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcademicYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Schedule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("LectureId");

                    b.ToTable("LectureSchedules");
                });

            modelBuilder.Entity("LectureManagement.Model.LectureStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcademicYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("LectureId");

                    b.ToTable("LectureStudents");
                });

            modelBuilder.Entity("LecturePrerequisite", b =>
                {
                    b.Property<Guid>("LectureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PrerequisiteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LectureId", "PrerequisiteId");

                    b.HasIndex("PrerequisiteId");

                    b.ToTable("LecturePrerequisite");
                });

            modelBuilder.Entity("LectureManagement.Model.LectureInstructor", b =>
                {
                    b.HasOne("LectureManagement.Model.AcademicYear", "AcademicYear")
                        .WithMany()
                        .HasForeignKey("AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureManagement.Model.Lecture", "Lecture")
                        .WithMany("Instructors")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("LectureManagement.Model.LectureSchedule", b =>
                {
                    b.HasOne("LectureManagement.Model.AcademicYear", "AcademicYear")
                        .WithMany()
                        .HasForeignKey("AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureManagement.Model.Lecture", "Lecture")
                        .WithMany("Schedules")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("LectureManagement.Model.LectureStudent", b =>
                {
                    b.HasOne("LectureManagement.Model.AcademicYear", "AcademicYear")
                        .WithMany()
                        .HasForeignKey("AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureManagement.Model.Lecture", "Lecture")
                        .WithMany("Students")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("LecturePrerequisite", b =>
                {
                    b.HasOne("LectureManagement.Model.Lecture", null)
                        .WithMany()
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("LectureManagement.Model.Lecture", null)
                        .WithMany()
                        .HasForeignKey("PrerequisiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LectureManagement.Model.Lecture", b =>
                {
                    b.Navigation("Instructors");

                    b.Navigation("Schedules");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
