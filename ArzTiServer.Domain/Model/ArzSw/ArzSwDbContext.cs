using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ArzSwDbContext : DbContext
    {
        public ArzSwDbContext()
        {
        }

        public ArzSwDbContext(DbContextOptions<ArzSwDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArzswBenutzer> ArzswBenutzers { get; set; } = null!;
        public virtual DbSet<ArzswDatenbank> ArzswDatenbanks { get; set; } = null!;
        public virtual DbSet<ArzswMandant> ArzswMandants { get; set; } = null!;
        public virtual DbSet<ArzswOption> ArzswOptions { get; set; } = null!;
        public virtual DbSet<ErezeptFhirPackage> ErezeptFhirPackages { get; set; } = null!;
        public virtual DbSet<ErezeptFhirPackageProfile> ErezeptFhirPackageProfiles { get; set; } = null!;
        public virtual DbSet<ErezeptFhirProfile> ErezeptFhirProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Username=postgres;Password=postgres;Server=localhost;Port=54321;Database=arzsw_db;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArzswBenutzer>(entity =>
            {
                entity.ToTable("arzsw_benutzer");

                entity.HasIndex(e => e.LoginName, "arzsw_benutzer_login_name_key")
                    .IsUnique();

                entity.HasIndex(e => e.LoginName, "idx_arzsw_benutzer_login_name")
                    .IsUnique();

                entity.Property(e => e.ArzswBenutzerId).HasColumnName("arzsw_benutzer_id");

                entity.Property(e => e.ArzswDatenbankId).HasColumnName("arzsw_datenbank_id");

                entity.Property(e => e.ArzswMandantId).HasColumnName("arzsw_mandant_id");

                entity.Property(e => e.BenutzerName)
                    .HasMaxLength(60)
                    .HasColumnName("benutzer_name")
                    .HasComment("Allgemeiner Name des Benutzers");

                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");

                entity.Property(e => e.LoginName)
                    .HasMaxLength(60)
                    .HasColumnName("login_name")
                    .HasComment("Login Name des Benutzers");

                entity.Property(e => e.LoginPasswort)
                    .HasMaxLength(60)
                    .HasColumnName("login_passwort");

                entity.Property(e => e.LoginPasswortCrypt)
                    .HasMaxLength(200)
                    .HasColumnName("login_passwort_crypt");

                entity.HasOne(d => d.ArzswDatenbank)
                    .WithMany(p => p.ArzswBenutzers)
                    .HasForeignKey(d => d.ArzswDatenbankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("arzsw_benutzer_arzsw_datenbank_id_fkey");

                entity.HasOne(d => d.ArzswMandant)
                    .WithMany(p => p.ArzswBenutzers)
                    .HasForeignKey(d => d.ArzswMandantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("arzsw_benutzer_arzsw_mandant_id_fkey");
            });

            modelBuilder.Entity<ArzswDatenbank>(entity =>
            {
                entity.ToTable("arzsw_datenbank");

                entity.HasIndex(e => e.DatenbankName, "arzsw_datenbank_datenbank_name_key")
                    .IsUnique();

                entity.HasIndex(e => e.DatenbankName, "idx_arzsw_datenbank_datenbank_name")
                    .IsUnique();

                entity.Property(e => e.ArzswDatenbankId).HasColumnName("arzsw_datenbank_id");

                entity.Property(e => e.ArzswMandantId).HasColumnName("arzsw_mandant_id");

                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");

                entity.Property(e => e.DatenbankAktiv)
                    .IsRequired()
                    .HasColumnName("datenbank_aktiv")
                    .HasDefaultValueSql("true")
                    .HasComment("Datenbank wird aktuell verwendet - z.B. kein Archiv, Backup, ...!");

                entity.Property(e => e.DatenbankConnectionString)
                    .HasMaxLength(200)
                    .HasColumnName("datenbank_connection_string")
                    .HasComment("Kompletter Connection-String für die Verbindung zur Datenbank");

                entity.Property(e => e.DatenbankName)
                    .HasMaxLength(60)
                    .HasColumnName("datenbank_name");

                entity.HasOne(d => d.ArzswMandant)
                    .WithMany(p => p.ArzswDatenbanks)
                    .HasForeignKey(d => d.ArzswMandantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("arzsw_datenbank_arzsw_mandant_id_fkey");
            });

            modelBuilder.Entity<ArzswMandant>(entity =>
            {
                entity.ToTable("arzsw_mandant");

                entity.HasIndex(e => e.CodeKenner, "arzsw_mandant_code_kenner_key")
                    .IsUnique();

                entity.HasIndex(e => e.CodeKenner, "idx_arzsw_mandant_code_kenner")
                    .IsUnique();

                entity.Property(e => e.ArzswMandantId).HasColumnName("arzsw_mandant_id");

                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");

                entity.Property(e => e.CodeKenner)
                    .HasMaxLength(20)
                    .HasColumnName("code_kenner")
                    .HasComment("Code-Kenner des Mandanten - muss eindeutig sein");

                entity.Property(e => e.MandantName)
                    .HasMaxLength(60)
                    .HasColumnName("mandant_name")
                    .HasComment("Allgemeiner name des Mandanten");
            });

            modelBuilder.Entity<ArzswOption>(entity =>
            {
                entity.ToTable("arzsw_option");

                entity.HasIndex(e => new { e.OptionTyp, e.OptionTypId }, "idx_arzsw_option_option_typ_option_typ_id")
                    .IsUnique();

                entity.Property(e => e.ArzswOptionId).HasColumnName("arzsw_option_id");

                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");

                entity.Property(e => e.Datentyp)
                    .HasMaxLength(20)
                    .HasColumnName("datentyp")
                    .HasComment("Unterstützte Datentypen - INTEGER | STRING | BOOL");

                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .HasColumnName("name");

                entity.Property(e => e.OptionTyp)
                    .HasMaxLength(60)
                    .HasColumnName("option_typ")
                    .HasComment("Zu welchen Typ gehört die Option - MANDANT | BENUTZER | DATENABNK");

                entity.Property(e => e.OptionTypId)
                    .HasColumnName("option_typ_id")
                    .HasComment("Zugehörige ID zum option_typ");

                entity.Property(e => e.Wert)
                    .HasMaxLength(200)
                    .HasColumnName("wert");
            });

            modelBuilder.Entity<ErezeptFhirPackage>(entity =>
            {
                entity.ToTable("erezept_fhir_package");

                entity.HasIndex(e => e.ErezeptFhirPackageVersion, "idx_erezept_fhir_package_erezept_fhir_package_version")
                    .IsUnique();

                entity.Property(e => e.ErezeptFhirPackageId).HasColumnName("erezept_fhir_package_id");

                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");

                entity.Property(e => e.ErezeptFhirPackageVersion)
                    .HasMaxLength(10)
                    .HasColumnName("erezept_fhir_package_version")
                    .HasComment("E-Rezept FHIR Package Version");

                entity.Property(e => e.GueltigBis).HasColumnName("gueltig_bis");

                entity.Property(e => e.GueltigVon).HasColumnName("gueltig_von");
            });

            modelBuilder.Entity<ErezeptFhirPackageProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("erezept_fhir_package_profile");

                entity.HasIndex(e => new { e.ErezeptFhirPackageId, e.QuellKenner }, "idx_erezept_fhir_package_profile_erezept_fhir_package_id_quell_")
                    .IsUnique();

                entity.Property(e => e.ErezeptFhirPackageId)
                    .HasColumnName("erezept_fhir_package_id")
                    .HasComment("Verknüpfung zur E-Rezept FHIR Package Version");

                entity.Property(e => e.ErezeptFhirProfileId)
                    .HasColumnName("erezept_fhir_profile_id")
                    .HasComment("Verknüpfung zur E-Rezept FHIR Profile Version");

                entity.Property(e => e.QuellKenner)
                    .HasMaxLength(10)
                    .HasColumnName("quell_kenner")
                    .HasComment("Verantwortliche Stelle für das FHIR-Profil - KBV|gematik|DAV|GKV|PKV|...");

                entity.HasOne(d => d.ErezeptFhirPackage)
                    .WithMany()
                    .HasForeignKey(d => d.ErezeptFhirPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("erezept_fhir_package_profile_erezept_fhir_package_id_fkey");

                entity.HasOne(d => d.ErezeptFhirProfile)
                    .WithMany()
                    .HasForeignKey(d => d.ErezeptFhirProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("erezept_fhir_package_profile_erezept_fhir_profile_id_fkey");
            });

            modelBuilder.Entity<ErezeptFhirProfile>(entity =>
            {
                entity.ToTable("erezept_fhir_profile");

                entity.HasIndex(e => new { e.ErezeptFhirProfileVersion, e.QuellKenner }, "idx_erezept_fhir_profile_erezept_fhir_profile_version_quell_ken")
                    .IsUnique();

                entity.Property(e => e.ErezeptFhirProfileId).HasColumnName("erezept_fhir_profile_id");

                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");

                entity.Property(e => e.ErezeptFhirProfileUrl)
                    .HasMaxLength(250)
                    .HasColumnName("erezept_fhir_profile_url");

                entity.Property(e => e.ErezeptFhirProfileVersion)
                    .HasMaxLength(10)
                    .HasColumnName("erezept_fhir_profile_version")
                    .HasComment("E-Rezept FHIR Profile Version");

                entity.Property(e => e.GueltigBis).HasColumnName("gueltig_bis");

                entity.Property(e => e.GueltigVon).HasColumnName("gueltig_von");

                entity.Property(e => e.QuellKenner)
                    .HasMaxLength(10)
                    .HasColumnName("quell_kenner");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
