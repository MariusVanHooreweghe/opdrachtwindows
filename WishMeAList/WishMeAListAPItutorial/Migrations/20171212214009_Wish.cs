using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class Wish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wish",
                columns: table => new
                {
                    WishID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    WishListID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wish", x => x.WishID);
                    table.ForeignKey(
                        name: "FK_Wish_WishList_WishListID",
                        column: x => x.WishListID,
                        principalTable: "WishList",
                        principalColumn: "WishListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wish_WishListID",
                table: "Wish",
                column: "WishListID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wish");
        }
    }
}
