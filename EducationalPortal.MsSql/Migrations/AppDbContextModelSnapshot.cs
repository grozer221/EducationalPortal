﻿// <auto-generated />
using System;
using EducationalPortal.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EducationalPortal.MsSql.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EducationalPortal.Business.Models.BackupModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique()
                        .HasFilter("[FileId] IS NOT NULL");

                    b.ToTable("Backups");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.EducationalYearModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("EducationalYears");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.FileModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HomeworkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("HomeworkId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.GradeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.HomeworkModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubjectPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectPostId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SettingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SubjectModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EducationalYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EducationalYearId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SubjectPostModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SubjectPosts");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GradeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("GradeId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GradeModelSubjectModel", b =>
                {
                    b.Property<Guid>("GradesHaveAccessReadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectsHaveAccessReadId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GradesHaveAccessReadId", "SubjectsHaveAccessReadId");

                    b.HasIndex("SubjectsHaveAccessReadId");

                    b.ToTable("GradeModelSubjectModel");
                });

            modelBuilder.Entity("SubjectModelUserModel", b =>
                {
                    b.Property<Guid>("SubjectHaveAccessCreatePostsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeachersHaveAccessCreatePostsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SubjectHaveAccessCreatePostsId", "TeachersHaveAccessCreatePostsId");

                    b.HasIndex("TeachersHaveAccessCreatePostsId");

                    b.ToTable("SubjectModelUserModel");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.BackupModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.FileModel", "File")
                        .WithOne("Backup")
                        .HasForeignKey("EducationalPortal.Business.Models.BackupModel", "FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("File");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.FileModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.UserModel", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("EducationalPortal.Business.Models.HomeworkModel", "Homework")
                        .WithMany("Files")
                        .HasForeignKey("HomeworkId");

                    b.Navigation("Creator");

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.HomeworkModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.UserModel", "Student")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EducationalPortal.Business.Models.SubjectPostModel", "SubjectPost")
                        .WithMany("Homeworks")
                        .HasForeignKey("SubjectPostId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Student");

                    b.Navigation("SubjectPost");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SubjectModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.EducationalYearModel", "EducationalYear")
                        .WithMany("Subjects")
                        .HasForeignKey("EducationalYearId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EducationalPortal.Business.Models.UserModel", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId");

                    b.Navigation("EducationalYear");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SubjectPostModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.SubjectModel", "Subject")
                        .WithMany("Posts")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EducationalPortal.Business.Models.UserModel", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.UserModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.GradeModel", "Grade")
                        .WithMany("Students")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("GradeModelSubjectModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.GradeModel", null)
                        .WithMany()
                        .HasForeignKey("GradesHaveAccessReadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationalPortal.Business.Models.SubjectModel", null)
                        .WithMany()
                        .HasForeignKey("SubjectsHaveAccessReadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectModelUserModel", b =>
                {
                    b.HasOne("EducationalPortal.Business.Models.SubjectModel", null)
                        .WithMany()
                        .HasForeignKey("SubjectHaveAccessCreatePostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationalPortal.Business.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("TeachersHaveAccessCreatePostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.EducationalYearModel", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.FileModel", b =>
                {
                    b.Navigation("Backup");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.GradeModel", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.HomeworkModel", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SubjectModel", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.SubjectPostModel", b =>
                {
                    b.Navigation("Homeworks");
                });

            modelBuilder.Entity("EducationalPortal.Business.Models.UserModel", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
