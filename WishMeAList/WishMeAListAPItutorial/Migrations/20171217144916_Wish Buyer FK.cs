using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class WishBuyerFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerUserID",
                table: "Wish",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wish_BuyerUserID",
                table: "Wish",
                column: "BuyerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Wish_User_BuyerUserID",
                table: "Wish",
                column: "BuyerUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wish_User_BuyerUserID",
                table: "Wish");

            migrationBuilder.DropIndex(
                name: "IX_Wish_BuyerUserID",
                table: "Wish");

            migrationBuilder.DropColumn(
                name: "BuyerUserID",
                table: "Wish");
        }
    }
}
