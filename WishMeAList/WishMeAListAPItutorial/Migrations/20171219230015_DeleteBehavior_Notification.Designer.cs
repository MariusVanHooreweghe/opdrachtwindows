﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WishMeAListAPItutorial.Data;
using WishMeAListAPItutorial.Models;

namespace WishMeAListAPItutorial.Migrations
{
    [DbContext(typeof(WishListContext))]
    [Migration("20171219230015_DeleteBehavior_Notification")]
    partial class DeleteBehavior_Notification
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
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

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Friendship", b =>
                {
                    b.Property<int>("BefrienderID");

                    b.Property<int>("BefriendedID");

                    b.HasKey("BefrienderID", "BefriendedID");

                    b.HasIndex("BefriendedID");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("HasBeenRead");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<int>("RecieverID");

                    b.Property<int>("SenderID");

                    b.Property<int>("Type");

                    b.Property<int?>("WishListID");

                    b.HasKey("NotificationID");

                    b.HasIndex("RecieverID");

                    b.HasIndex("SenderID");

                    b.HasIndex("WishListID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Wish", b =>
                {
                    b.Property<int>("WishID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuyerID");

                    b.Property<int?>("BuyerUserID");

                    b.Property<int>("Categorie");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsChecked");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("WishListID");

                    b.HasKey("WishID");

                    b.HasIndex("BuyerID");

                    b.HasIndex("BuyerUserID");

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

                    b.HasIndex("OwnerID");

                    b.ToTable("WishList");
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.WishListAccessor", b =>
                {
                    b.Property<int>("WishListID");

                    b.Property<int>("UserID");

                    b.HasKey("WishListID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("TTWishListAccessor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Friendship", b =>
                {
                    b.HasOne("WishMeAListAPItutorial.Models.User", "Befriended")
                        .WithMany()
                        .HasForeignKey("BefriendedID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WishMeAListAPItutorial.Models.User", "Befriender")
                        .WithMany()
                        .HasForeignKey("BefrienderID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Notification", b =>
                {
                    b.HasOne("WishMeAListAPItutorial.Models.User", "Reciever")
                        .WithMany("Notifications")
                        .HasForeignKey("RecieverID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WishMeAListAPItutorial.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WishMeAListAPItutorial.Models.WishList", "WishList")
                        .WithMany()
                        .HasForeignKey("WishListID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.Wish", b =>
                {
                    b.HasOne("WishMeAListAPItutorial.Models.User")
                        .WithMany("WishesBuying")
                        .HasForeignKey("BuyerID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WishMeAListAPItutorial.Models.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerUserID");

                    b.HasOne("WishMeAListAPItutorial.Models.WishList")
                        .WithMany("Wishes")
                        .HasForeignKey("WishListID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.WishList", b =>
                {
                    b.HasOne("WishMeAListAPItutorial.Models.User")
                        .WithMany("WishListsOwning")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WishMeAListAPItutorial.Models.WishListAccessor", b =>
                {
                    b.HasOne("WishMeAListAPItutorial.Models.User", "User")
                        .WithMany("WishListsAccessing")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WishMeAListAPItutorial.Models.WishList", "WishList")
                        .WithMany("Accessors")
                        .HasForeignKey("WishListID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
