using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "User",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_GradeId",
                table: "User",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Grade_GradeId",
                table: "User",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Grade_GradeId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_GradeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "User");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "User",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
