using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherLessonId",
                table: "LessonHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TeacherLesson",
                columns: table => new
                {
                    TeacherLessonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartUp = table.Column<string>(type: "text", nullable: false),
                    Knowledge = table.Column<string>(type: "text", nullable: false),
                    Practice = table.Column<string>(type: "text", nullable: false),
                    Apply = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RejectedCount = table.Column<int>(type: "integer", nullable: false),
                    DisapprovedReason = table.Column<string>(type: "text", nullable: false),
                    PromptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLesson", x => x.TeacherLessonId);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompt",
                        principalColumn: "PromptId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonHistory_TeacherLessonId",
                table: "LessonHistory",
                column: "TeacherLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_PromptId",
                table: "TeacherLesson",
                column: "PromptId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonHistory_TeacherLesson_TeacherLessonId",
                table: "LessonHistory",
                column: "TeacherLessonId",
                principalTable: "TeacherLesson",
                principalColumn: "TeacherLessonId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonHistory_TeacherLesson_TeacherLessonId",
                table: "LessonHistory");

            migrationBuilder.DropTable(
                name: "TeacherLesson");

            migrationBuilder.DropIndex(
                name: "IX_LessonHistory_TeacherLessonId",
                table: "LessonHistory");

            migrationBuilder.DropColumn(
                name: "TeacherLessonId",
                table: "LessonHistory");
        }
    }
}
