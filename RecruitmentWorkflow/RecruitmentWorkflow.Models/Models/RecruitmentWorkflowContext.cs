using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RecruitmentWorkflow.Models.Models.WorkflowData;

namespace RecruitmentWorkflow.Models.Models;

public partial class RecruitmentWorkflowContext : DbContext
{
    public RecruitmentWorkflowContext()
    {
    }

    public RecruitmentWorkflowContext(DbContextOptions<RecruitmentWorkflowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CandidateStatusMaster> CandidateStatusMasters { get; set; }
    public virtual DbSet<EmployeeRoleMaster> EmployeeRoleMasters { get; set; }
    public virtual DbSet<InterviewStatusMaster> InterviewStatusMasters { get; set; }
    public virtual DbSet<RecruitmentStageMaster> RecruitmentStageMasters { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Job> Jobs { get; set; }
    public virtual DbSet<Candidate> Candidates { get; set; }
    public virtual DbSet<JobCandidates> JobsCandidates { get; set; }
    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<MyCandidateData> MyCandidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CandidateStatusMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC076177A841");

            entity.ToTable("CandidateStatusMaster");

            entity.HasIndex(e => e.Status, "UQ__Candidat__3A15923FE89B1936").IsUnique();

            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeRoleMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC073A611E34");

            entity.ToTable("EmployeeRoleMaster");

            entity.HasIndex(e => e.Name, "UQ__Employee__737584F6385BDBEC").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<InterviewStatusMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC07A4211FC5");

            entity.ToTable("InterviewStatusMaster");

            entity.HasIndex(e => e.Status, "UQ__Intervie__3A15923FA620AEE9").IsUnique();

            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<RecruitmentStageMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recruitm__3214EC0797E0C968");

            entity.ToTable("RecruitmentStageMaster");

            entity.HasIndex(e => e.Stage, "UQ__Recruitm__BA80465BBF59DEDC").IsUnique();

            entity.Property(e => e.Stage).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Job>()
            .Property(j => j.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1000, 1);

        modelBuilder.Entity<Job>()
            .Property(j => j.PostedDate)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Candidate>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(10000, 1);

        modelBuilder.Entity<Candidate>()
            .Property(c => c.OverAllRating)
            .HasPrecision(3, 1);

        modelBuilder.Entity<JobCandidates>()
            .HasKey(jc => new { jc.JobId, jc.CandidateId });

        modelBuilder.Entity<Interview>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(500, 1);

        modelBuilder.Entity<MyCandidateData>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
