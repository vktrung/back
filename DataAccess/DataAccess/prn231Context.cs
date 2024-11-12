using System;
using System.Collections.Generic;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DataAccess
{
    public partial class prn231Context : DbContext
    {
        public prn231Context()
        {
        }

        public prn231Context(DbContextOptions<prn231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ComparisonType> ComparisonTypes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<GradeType> GradeTypes { get; set; } = null!;
        public virtual DbSet<PassCondition> PassConditions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<SessionStudent> SessionStudents { get; set; } = null!;
        public virtual DbSet<StudentGrade> StudentGrades { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                String ConnectionStr = config.GetConnectionString("DB");

                optionsBuilder.UseSqlServer(ConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.HasIndex(e => e.Name, "UQ__Class__72E12F1B079CD921")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ComparisonType>(entity =>
            {
                entity.ToTable("ComparisonType");

                entity.HasIndex(e => e.Name, "UQ__Comparis__72E12F1B5FFF7F29")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.Code, "UQ__Course__357D4CF94DDC8427")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Course__72E12F1B6BEBC001")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.GradeTypeId).HasColumnName("gradeTypeId");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__courseId__571DF1D5");

                entity.HasOne(d => d.GradeType)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.GradeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__gradeType__5812160E");
            });

            modelBuilder.Entity<GradeType>(entity =>
            {
                entity.ToTable("GradeType");

                entity.HasIndex(e => e.Name, "UQ__GradeTyp__72E12F1BFCF00FB3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GradedByRole).HasColumnName("gradedByRole");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PassConditionId).HasColumnName("passConditionId");

                entity.HasOne(d => d.GradedByRoleNavigation)
                    .WithMany(p => p.GradeTypes)
                    .HasForeignKey(d => d.GradedByRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeType__grade__5441852A");

                entity.HasOne(d => d.PassCondition)
                    .WithMany(p => p.GradeTypes)
                    .HasForeignKey(d => d.PassConditionId)
                    .HasConstraintName("FK__GradeType__passC__534D60F1");
            });

            modelBuilder.Entity<PassCondition>(entity =>
            {
                entity.ToTable("PassCondition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComparisonTypeId).HasColumnName("comparisonTypeId");

                entity.Property(e => e.GradeValue).HasColumnName("gradeValue");

                entity.HasOne(d => d.ComparisonType)
                    .WithMany(p => p.PassConditions)
                    .HasForeignKey(d => d.ComparisonTypeId)
                    .HasConstraintName("FK__PassCondi__compa__4F7CD00D");
            });





            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Semester");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.TeahcerId).HasColumnName("teahcerId");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__classId__45F365D3");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__courseI__440B1D61");

                entity.HasOne(d => d.Teahcer)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.TeahcerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__teahcer__44FF419A");
            });

            modelBuilder.Entity<SessionStudent>(entity =>
            {
                entity.HasKey(e => new { e.SessionId, e.StudentId })
                    .HasName("PK__SessionS__F70A0F48963274F8");

                entity.ToTable("SessionStudent");

                entity.Property(e => e.SessionId).HasColumnName("sessionId");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.AvgGragde)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("avgGragde");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionStudents)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionSt__sessi__48CFD27E");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SessionStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionSt__stude__49C3F6B7");
            });

            modelBuilder.Entity<StudentGrade>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.GradeId })
                    .HasName("PK__StudentG__C2A5E01316895A60");

                entity.ToTable("StudentGrade");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.GradeId).HasColumnName("gradeId");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("value");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentGr__grade__5BE2A6F2");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentGr__stude__5AEE82B9");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__roleId__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
