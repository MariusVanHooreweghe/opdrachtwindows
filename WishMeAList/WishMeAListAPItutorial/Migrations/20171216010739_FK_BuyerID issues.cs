using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class FK_BuyerIDissues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuyerID",
                table: "Wish",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuyerID",
                table: "Wish",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
