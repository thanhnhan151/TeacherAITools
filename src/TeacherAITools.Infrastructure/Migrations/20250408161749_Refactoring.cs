using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Lesson");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Requirement",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Blog",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_LessonId",
                table: "Blog",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Lesson_LessonId",
                table: "Blog",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Lesson_LessonId",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_LessonId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Blog");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Requirement",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Lesson",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Lesson",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
