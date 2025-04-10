using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeacherLesson",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "LessonHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SchoolSupply",
                table: "LessonHistory",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeacherLesson");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "LessonHistory");

            migrationBuilder.DropColumn(
                name: "SchoolSupply",
                table: "LessonHistory");
        }
    }
}
