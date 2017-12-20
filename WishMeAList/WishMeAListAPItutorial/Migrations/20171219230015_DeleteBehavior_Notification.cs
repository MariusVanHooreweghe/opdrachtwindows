using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class DeleteBehavior_Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_WishList_WishListID",
                table: "Notification");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_WishList_WishListID",
                table: "Notification",
                column: "WishListID",
                principalTable: "WishList",
                principalColumn: "WishListID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_WishList_WishListID",
                table: "Notification");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_WishList_WishListID",
                table: "Notification",
                column: "WishListID",
                principalTable: "WishList",
                principalColumn: "WishListID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
