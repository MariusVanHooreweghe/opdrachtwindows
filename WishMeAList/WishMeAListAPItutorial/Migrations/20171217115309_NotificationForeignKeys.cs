using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishMeAListAPItutorial.Migrations
{
    public partial class NotificationForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_RecieverUserID",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_SenderUserID",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_UserID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_RecieverUserID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_SenderUserID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "RecieverUserID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SenderUserID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "WishListID",
                table: "Notification",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RecieverID",
                table: "Notification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderID",
                table: "Notification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_RecieverID",
                table: "Notification",
                column: "RecieverID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SenderID",
                table: "Notification",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_RecieverID",
                table: "Notification",
                column: "RecieverID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_SenderID",
                table: "Notification",
                column: "SenderID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_RecieverID",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_SenderID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_RecieverID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_SenderID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "RecieverID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "WishListID",
                table: "Notification",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecieverUserID",
                table: "Notification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderUserID",
                table: "Notification",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_RecieverUserID",
                table: "Notification",
                column: "RecieverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SenderUserID",
                table: "Notification",
                column: "SenderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_RecieverUserID",
                table: "Notification",
                column: "RecieverUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_SenderUserID",
                table: "Notification",
                column: "SenderUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserID",
                table: "Notification",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
