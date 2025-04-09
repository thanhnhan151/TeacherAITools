using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLesson_Prompt_PromptId",
                table: "TeacherLesson");

            migrationBuilder.DropIndex(
                name: "IX_TeacherLesson_PromptId",
                table: "TeacherLesson");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_PromptId",
                table: "TeacherLesson",
                column: "PromptId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLesson_Prompt_PromptId",
                table: "TeacherLesson",
                column: "PromptId",
                principalTable: "Prompt",
                principalColumn: "PromptId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLesson_Prompt_PromptId",
                table: "TeacherLesson");

            migrationBuilder.DropIndex(
                name: "IX_TeacherLesson_PromptId",
                table: "TeacherLesson");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_PromptId",
                table: "TeacherLesson",
                column: "PromptId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLesson_Prompt_PromptId",
                table: "TeacherLesson",
                column: "PromptId",
                principalTable: "Prompt",
                principalColumn: "PromptId");
        }
    }
}
