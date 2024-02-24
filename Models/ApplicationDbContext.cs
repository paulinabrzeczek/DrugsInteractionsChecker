using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterakcjeMiedzyLekami.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveSubstance> ActiveSubstances { get; set; } = null!;
        public virtual DbSet<ActiveSubstancesInteraction> ActiveSubstancesInteractions { get; set; } = null!;
        public virtual DbSet<OtherSubstance> OtherSubstances { get; set; } = null!;
        public virtual DbSet<Pharmaceutical> Pharmaceuticals { get; set; } = null!;
        public virtual DbSet<PharmaceuticalsActiveSubstance> PharmaceuticalsActiveSubstances { get; set; } = null!;
        public virtual DbSet<PharmaceuticalsCategory> PharmaceuticalsCategories { get; set; } = null!;
        public virtual DbSet<PharmaceuticalsReview> PharmaceuticalsReviews { get; set; } = null!;
        public virtual DbSet<PharmaceuticalsSubstanceInteraction> PharmaceuticalsSubstanceInteractions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\PAULINASQLSERVER;Database=project;Trusted_Connection=true;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveSubstance>(entity =>
            {
                entity.HasKey(e => e.IdActiveSubstance)
                    .HasName("PK_Active_Substances");

                entity.Property(e => e.NameActiveSubstance).HasMaxLength(255);
            });

            modelBuilder.Entity<ActiveSubstancesInteraction>(entity =>
            {
                entity.HasKey(e => e.IdInteractionSubstance)
                    .HasName("PK_Active_Substance_Interactions ");

                entity.ToTable("ActiveSubstancesInteractions ");

                entity.Property(e => e.DescriptionInteraction).HasMaxLength(255);

                entity.Property(e => e.TypeReaction).HasMaxLength(255);

                entity.HasOne(d => d.IdActiveSubstance1Navigation)
                    .WithMany(p => p.ActiveSubstancesInteractionIdActiveSubstance1Navigations)
                    .HasForeignKey(d => d.IdActiveSubstance1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActiveSubstancesInteractions_ActiveSubstances1");

                entity.HasOne(d => d.IdActiveSubstance2Navigation)
                    .WithMany(p => p.ActiveSubstancesInteractionIdActiveSubstance2Navigations)
                    .HasForeignKey(d => d.IdActiveSubstance2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActiveSubstancesInteractions_ActiveSubstances2");
            });

            modelBuilder.Entity<OtherSubstance>(entity =>
            {
                entity.HasKey(e => e.IdSubstance)
                    .HasName("PK_Other_Substances");

                entity.Property(e => e.NameSubstance).HasMaxLength(50);
            });

            modelBuilder.Entity<Pharmaceutical>(entity =>
            {
                entity.HasKey(e => e.IdPharmaceutical);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Manufacturer).HasMaxLength(255);

                entity.Property(e => e.NamePharmaceutical).HasMaxLength(255);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Pharmaceuticals)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pharmaceuticals_Pharmaceuticals_Categories");
            });

            modelBuilder.Entity<PharmaceuticalsActiveSubstance>(entity =>
            {
                entity.HasKey(e => new { e.IdPharmaceutical, e.IdActiveSubstance })
                    .HasName("PK_Pharmaceuticals_Active_Substance _1");

                entity.ToTable("PharmaceuticalsActiveSubstance ");

                entity.Property(e => e.DoseSubstance).HasComment("mg");

                entity.HasOne(d => d.IdActiveSubstanceNavigation)
                    .WithMany(p => p.PharmaceuticalsActiveSubstances)
                    .HasForeignKey(d => d.IdActiveSubstance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pharmaceuticals_Active_Substance_Active_Substances");

                entity.HasOne(d => d.IdPharmaceuticalNavigation)
                    .WithMany(p => p.PharmaceuticalsActiveSubstances)
                    .HasForeignKey(d => d.IdPharmaceutical)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pharmaceuticals_Active_Substance_Pharmaceuticals");
            });

            modelBuilder.Entity<PharmaceuticalsCategory>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK_Pharmaceuticals_Categories");

                entity.Property(e => e.NameCategory).HasMaxLength(255);
            });

            modelBuilder.Entity<PharmaceuticalsReview>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PK_Pharmacy_Reviews");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Review).HasMaxLength(255);

                entity.HasOne(d => d.IdPharmaceuticalNavigation)
                    .WithMany(p => p.PharmaceuticalsReviews)
                    .HasForeignKey(d => d.IdPharmaceutical)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pharmaceuticals_Reviews_Pharmaceuticals");

                entity.HasMany(d => d.IdUsers)
                    .WithMany(p => p.IdReviews)
                    .UsingEntity<Dictionary<string, object>>(
                        "PharmaceuticalsReviewsUser",
                        l => l.HasOne<User>().WithMany().HasForeignKey("IdUser").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User_Pharmaceuticals_Rieviews_Users"),
                        r => r.HasOne<PharmaceuticalsReview>().WithMany().HasForeignKey("IdReview").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PharmaceuticalsReviews_PharmaceuticalsReviewsUsers"),
                        j =>
                        {
                            j.HasKey("IdReview", "IdUser").HasName("PK_Pharmaceuticals_Reviews_Users_1");

                            j.ToTable("PharmaceuticalsReviewsUsers");
                        });
            });

            modelBuilder.Entity<PharmaceuticalsSubstanceInteraction>(entity =>
            {
                entity.HasKey(e => e.IdInteraction)
                    .HasName("PK_Pharmaceuticals_Substance_Interactions");

                entity.Property(e => e.DescriptionInteraction).HasMaxLength(255);

                entity.Property(e => e.TypeReaction).HasMaxLength(255);

                entity.HasOne(d => d.IdPharmaceuticalNavigation)
                    .WithMany(p => p.PharmaceuticalsSubstanceInteractions)
                    .HasForeignKey(d => d.IdPharmaceutical)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PharmaceuticalsSubstanceInteractions_Pharmaceuticals");

                entity.HasOne(d => d.IdSubstanceNavigation)
                    .WithMany(p => p.PharmaceuticalsSubstanceInteractions)
                    .HasForeignKey(d => d.IdSubstance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PharmaceuticalsSubstanceInteractions_OtherSubstances");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK_Role");

                entity.Property(e => e.NameRole).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Passwordhash).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
