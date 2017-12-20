using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class DeleteBehavior_Accessors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTWishListAccessor_User_UserID",
                table: "TTWishListAccessor");

            migrationBuilder.DropForeignKey(
                name: "FK_Wish_WishList_WishListID",
                table: "Wish");

            migrationBuilder.AddForeignKey(
                name: "FK_TTWishListAccessor_User_UserID",
                table: "TTWishListAccessor",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_WishList_WishListID",
                table: "Wish",
                column: "WishListID",
                principalTable: "WishList",
                principalColumn: "WishListID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTWishListAccessor_User_UserID",
                table: "TTWishListAccessor");

            migrationBuilder.DropForeignKey(
                name: "FK_Wish_WishList_WishListID",
                table: "Wish");

            migrationBuilder.AddForeignKey(
                name: "FK_TTWishListAccessor_User_UserID",
                table: "TTWishListAccessor",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_WishList_WishListID",
                table: "Wish",
                column: "WishListID",
                principalTable: "WishList",
                principalColumn: "WishListID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
