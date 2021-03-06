﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WishMeAListAPItutorial.Data;

namespace WishMeAListAPItutorial.Migrations
{
    [DbContext(typeof(WishListContext))]
    [Migration("20171212214009_Wish")]
    partial class Wish
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotification", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedNotificationName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("NotificationName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedNotificationName")
                        .IsUnique()
                        .HasName("NotificationNameIndex")
                        .HasFilter("[NormalizedNotificationName] IS NOT NULL");

                    b.ToTable("AspNetNotifications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("NotificationId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.ToTable("AspNetNotificationClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("NotificationId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("NotificationId");

                    b.ToTable("AspNetNotificationLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationRole<string>", b =>
                {
                    b.Property<string>("NotificationId");

                    b.Property<string>("RoleId");

                    b.HasKey("NotificationId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetNotificationRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationToken<string>", b =>
                {
                    b.Property<string>("NotificationId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("NotificationId", "LoginProvider", "Name");

                    b.ToTable("AspNetNotificationTokens");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Wish", b =>
                {
                    b.Property<int>("WishID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsChecked");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("WishListID");

                    b.HasKey("WishID");

                    b.HasIndex("WishListID");

                    b.ToTable("Wish");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.WishList", b =>
                {
                    b.Property<int>("WishListID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfEvent");

                    b.Property<int>("OwnerID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("WishListID");

                    b.ToTable("WishList");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityNotification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityNotification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityNotification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityNotificationToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityNotification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Wish", b =>
                {
                    b.HasOne("WishMeAListAPItutorial.Models.WishList")
                        .WithMany("Wishes")
                        .HasForeignKey("WishListID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
