﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Online_Learning_Platform.AllDbContext;

#nullable disable

namespace Online_Learning_Platform.Migrations
{
    [DbContext(typeof(AllTheDbContext))]
    partial class AllTheDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Online_Learning_Platform.Model.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CourseDescription")
                        .HasColumnType("text");

                    b.Property<string>("CourseName")
                        .HasColumnType("text");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Online_Learning_Platform.Model.Enrollment", b =>
                {
                    b.Property<Guid>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Progress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Online_Learning_Platform.Model.Instructor", b =>
                {
                    b.Property<Guid>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Expertise")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InstructorName")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric");

                    b.HasKey("InstructorId");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("Online_Learning_Platform.Model.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ReviewId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Online_Learning_Platform.Model.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("MobileNo")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Online_Learning_Platform.Model.Enrollment", b =>
                {
                    b.HasOne("Online_Learning_Platform.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Online_Learning_Platform.Model.Review", b =>
                {
                    b.HasOne("Online_Learning_Platform.Model.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("Online_Learning_Platform.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Course");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
