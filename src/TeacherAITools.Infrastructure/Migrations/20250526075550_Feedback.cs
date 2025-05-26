using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Feedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Prompt",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "LessonPlan",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apply",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Lesson",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Knowledge",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Practice",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SchoolSupply",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartUp",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CurriculumFeedback",
                columns: table => new
                {
                    CurriculumFeedBackId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Body = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CurriculumId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumFeedback", x => x.CurriculumFeedBackId);
                    table.ForeignKey(
                        name: "FK_CurriculumFeedback_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CurriculumFeedback_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_UserId",
                table: "Prompt",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumFeedback_CurriculumId",
                table: "CurriculumFeedback",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumFeedback_UserId",
                table: "CurriculumFeedback",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_User_UserId",
                table: "Prompt",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_User_UserId",
                table: "Prompt");

            migrationBuilder.DropTable(
                name: "CurriculumFeedback");

            migrationBuilder.DropIndex(
                name: "IX_Prompt_UserId",
                table: "Prompt");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Prompt");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "LessonPlan");

            migrationBuilder.DropColumn(
                name: "Apply",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Knowledge",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Practice",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "SchoolSupply",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "StartUp",
                table: "Lesson");
        }
    }
}
