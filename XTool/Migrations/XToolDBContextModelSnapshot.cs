﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using XTool.Data;
using XTool.Models.ActorModels;
using XTool.Models.Shared;

namespace XTool.Migrations
{
    [DbContext(typeof(XToolDbContext))]
    partial class XToolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Position")
                        .HasMaxLength(500);

                    b.Property<int>("Priority");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.BiographyEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("BiographyEvents");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.CareerEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CareerPeriodId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("TimePeriodId");

                    b.HasKey("Id");

                    b.HasIndex("CareerPeriodId");

                    b.ToTable("CareerEvents");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.CareerPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Period")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("CareerPeriods");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.CustomSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("CustomSections");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Description")
                        .HasMaxLength(2000);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4096);

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Url")
                        .HasMaxLength(4096);

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Text")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Quotations");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<string>("Description")
                        .HasMaxLength(2000);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4096);

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("XTool.Models.EvaluationModels.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("Comment")
                        .HasMaxLength(2000);

                    b.Property<int?>("ExpertId");

                    b.Property<DateTime>("LastChange");

                    b.Property<int>("ScalesId");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("ExpertId");

                    b.HasIndex("ScalesId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("XTool.Models.EvaluationModels.Scales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Depression");

                    b.Property<int>("Hypochondriasis");

                    b.Property<int>("Hypomania");

                    b.Property<int>("Hysteria");

                    b.Property<int>("MasculinityFeminity");

                    b.Property<int>("Paranoia");

                    b.Property<int>("Psychasthenia");

                    b.Property<int>("PsychopathicDeviate");

                    b.Property<int>("Schizophrenia");

                    b.Property<int>("SocialInteroversion");

                    b.HasKey("Id");

                    b.ToTable("Scales");
                });

            modelBuilder.Entity("XTool.Models.Roles.XToolRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("XToolRole");
                });

            modelBuilder.Entity("XTool.Models.Roles.XToolUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsBanned");

                    b.Property<bool>("IsConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Position");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("XTool.Models.UploadedPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UploadedPhotos");
                });

            modelBuilder.Entity("XTool.Models.Roles.AdminRole", b =>
                {
                    b.HasBaseType("XTool.Models.Roles.XToolRole");

                    b.Property<int>("Level");

                    b.ToTable("AdminRole");

                    b.HasDiscriminator().HasValue("AdminRole");
                });

            modelBuilder.Entity("XTool.Models.Roles.ExpertRole", b =>
                {
                    b.HasBaseType("XTool.Models.Roles.XToolRole");


                    b.ToTable("ExpertRole");

                    b.HasDiscriminator().HasValue("ExpertRole");
                });

            modelBuilder.Entity("XTool.Models.Roles.SuperadminRole", b =>
                {
                    b.HasBaseType("XTool.Models.Roles.XToolRole");


                    b.ToTable("SuperadminRole");

                    b.HasDiscriminator().HasValue("SuperadminRole");
                });

            modelBuilder.Entity("XTool.Models.Roles.TechnologistRole", b =>
                {
                    b.HasBaseType("XTool.Models.Roles.XToolRole");


                    b.ToTable("TechnologistRole");

                    b.HasDiscriminator().HasValue("TechnologistRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("XTool.Models.Roles.XToolRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("XTool.Models.Roles.XToolUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("XTool.Models.Roles.XToolUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("XTool.Models.Roles.XToolRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XTool.Models.Roles.XToolUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("XTool.Models.Roles.XToolUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.BiographyEvent", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("BiograpphyEvents")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.CareerEvent", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.CareerPeriod")
                        .WithMany("CareerEvents")
                        .HasForeignKey("CareerPeriodId");
                });

            modelBuilder.Entity("XTool.Models.ActorModels.CareerPeriod", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("CareerPeriods")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.CustomSection", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("CustomSection")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Photo", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("Photos")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Publication", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("Publications")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Quotation", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("Quotations")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.ActorModels.Video", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor")
                        .WithMany("Videos")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.EvaluationModels.Evaluation", b =>
                {
                    b.HasOne("XTool.Models.ActorModels.Actor", "Actor")
                        .WithMany("Evaluations")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XTool.Models.Roles.XToolUser", "Expert")
                        .WithMany()
                        .HasForeignKey("ExpertId");

                    b.HasOne("XTool.Models.EvaluationModels.Scales", "Scales")
                        .WithMany()
                        .HasForeignKey("ScalesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XTool.Models.UploadedPhoto", b =>
                {
                    b.HasOne("XTool.Models.Roles.XToolUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
