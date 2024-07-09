﻿// <auto-generated />
using System;
using G2T.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace webapiG2T.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240709180648_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("G2T.Models.Canal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Canaux");
                });

            modelBuilder.Entity("G2T.Models.Compte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOuverture")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomCompte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Solde")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeCompte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("G2T.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompteId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompteId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("G2T.Models.EntiteEnCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomEntiteEnCharge")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EntiteEnCharge");
                });

            modelBuilder.Entity("G2T.Models.Facture", b =>
                {
                    b.Property<int>("CompteId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateExpiration")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFacturation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePaiement")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Montant")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompteId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("G2T.Models.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CanalId")
                        .HasColumnType("int");

                    b.Property<string>("Commentaire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntiteEnChargeId")
                        .HasColumnType("int");

                    b.Property<int>("MotifId")
                        .HasColumnType("int");

                    b.Property<int>("SousMotifId")
                        .HasColumnType("int");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CanalId");

                    b.HasIndex("EntiteEnChargeId");

                    b.HasIndex("MotifId");

                    b.HasIndex("SousMotifId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("G2T.Models.Motif", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Motifs");
                });

            modelBuilder.Entity("G2T.Models.Profil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profils");
                });

            modelBuilder.Entity("G2T.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NomService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("G2T.Models.SousMotif", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SousMotifs");
                });

            modelBuilder.Entity("G2T.Models.TypeService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeServices");
                });

            modelBuilder.Entity("G2T.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("EntiteEnChargeId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfilId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntiteEnChargeId");

                    b.HasIndex("ProfilId");

                    b.ToTable("Utilisateur");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("G2T.Models.Contact", b =>
                {
                    b.HasOne("G2T.Models.Compte", "Compte")
                        .WithMany("Contacts")
                        .HasForeignKey("CompteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");
                });

            modelBuilder.Entity("G2T.Models.Facture", b =>
                {
                    b.HasOne("G2T.Models.Compte", "Compte")
                        .WithMany("Factures")
                        .HasForeignKey("CompteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G2T.Models.Service", "Service")
                        .WithMany("Factures")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("G2T.Models.Incident", b =>
                {
                    b.HasOne("G2T.Models.Canal", "Canal")
                        .WithMany("Incidents")
                        .HasForeignKey("CanalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G2T.Models.EntiteEnCharge", "EntiteEnCharge")
                        .WithMany("Incidents")
                        .HasForeignKey("EntiteEnChargeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G2T.Models.Motif", "Motif")
                        .WithMany("Incidents")
                        .HasForeignKey("MotifId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G2T.Models.SousMotif", "SousMotif")
                        .WithMany("Incidents")
                        .HasForeignKey("SousMotifId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canal");

                    b.Navigation("EntiteEnCharge");

                    b.Navigation("Motif");

                    b.Navigation("SousMotif");
                });

            modelBuilder.Entity("G2T.Models.Service", b =>
                {
                    b.HasOne("G2T.Models.TypeService", "TypeService")
                        .WithMany("Services")
                        .HasForeignKey("TypeServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeService");
                });

            modelBuilder.Entity("G2T.Models.Utilisateur", b =>
                {
                    b.HasOne("G2T.Models.EntiteEnCharge", "EntiteEnCharge")
                        .WithMany("Utilisateurs")
                        .HasForeignKey("EntiteEnChargeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G2T.Models.Profil", "Profil")
                        .WithMany("Utilisateurs")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntiteEnCharge");

                    b.Navigation("Profil");
                });

            modelBuilder.Entity("G2T.Models.Canal", b =>
                {
                    b.Navigation("Incidents");
                });

            modelBuilder.Entity("G2T.Models.Compte", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Factures");
                });

            modelBuilder.Entity("G2T.Models.EntiteEnCharge", b =>
                {
                    b.Navigation("Incidents");

                    b.Navigation("Utilisateurs");
                });

            modelBuilder.Entity("G2T.Models.Motif", b =>
                {
                    b.Navigation("Incidents");
                });

            modelBuilder.Entity("G2T.Models.Profil", b =>
                {
                    b.Navigation("Utilisateurs");
                });

            modelBuilder.Entity("G2T.Models.Service", b =>
                {
                    b.Navigation("Factures");
                });

            modelBuilder.Entity("G2T.Models.SousMotif", b =>
                {
                    b.Navigation("Incidents");
                });

            modelBuilder.Entity("G2T.Models.TypeService", b =>
                {
                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
