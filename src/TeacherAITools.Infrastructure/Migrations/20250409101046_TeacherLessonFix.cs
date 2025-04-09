using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeacherLessonFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_User_UserId",
                table: "Prompt");

            migrationBuilder.DropIndex(
                name: "IX_Prompt_UserId",
                table: "Prompt");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Prompt");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lesson");

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "TeacherLesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SchoolSupply",
                table: "TeacherLesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TeacherLesson",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_UserId",
                table: "TeacherLesson",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLesson_User_UserId",
                table: "TeacherLesson",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLesson_User_UserId",
                table: "TeacherLesson");

            migrationBuilder.DropIndex(
                name: "IX_TeacherLesson_UserId",
                table: "TeacherLesson");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "TeacherLesson");

            migrationBuilder.DropColumn(
                name: "SchoolSupply",
                table: "TeacherLesson");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeacherLesson");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Prompt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_UserId",
                table: "Prompt",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_User_UserId",
                table: "Prompt",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
