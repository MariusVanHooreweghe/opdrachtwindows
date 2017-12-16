using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class UserAndWishListAccessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerID",
                table: "Wish",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "TTWishListAccessor",
                columns: table => new
                {
                    WishListID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTWishListAccessor", x => new { x.WishListID, x.UserID });
                    table.ForeignKey(
                        name: "FK_TTWishListAccessor_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TTWishListAccessor_WishList_WishListID",
                        column: x => x.WishListID,
                        principalTable: "WishList",
                        principalColumn: "WishListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishList_OwnerID",
                table: "WishList",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_BuyerID",
                table: "Wish",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_TTWishListAccessor_UserID",
                table: "TTWishListAccessor",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_User_BuyerID",
                table: "Wish",
                column: "BuyerID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_User_OwnerID",
                table: "WishList",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wish_User_BuyerID",
                table: "Wish");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_User_OwnerID",
                table: "WishList");

            migrationBuilder.DropTable(
                name: "TTWishListAccessor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_WishList_OwnerID",
                table: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_Wish_BuyerID",
                table: "Wish");

            migrationBuilder.DropColumn(
                name: "BuyerID",
                table: "Wish");
        }
    }
}
