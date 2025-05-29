using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apply",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Lesson");

            migrationBuilder.RenameColumn(
                name: "StartUp",
                table: "Lesson",
                newName: "SpecialAbility");

            migrationBuilder.RenameColumn(
                name: "Practice",
                table: "Lesson",
                newName: "Quality");

            migrationBuilder.RenameColumn(
                name: "Knowledge",
                table: "Lesson",
                newName: "GeneralCapacity");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "LessonPlan",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Lesson",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);

            migrationBuilder.CreateTable(
                name: "Apply",
                columns: table => new
                {
                    ApplyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    TeacherActivities = table.Column<string>(type: "text", nullable: false),
                    StudentActivities = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: true),
                    PromptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apply", x => x.ApplyId);
                    table.ForeignKey(
                        name: "FK_Apply_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId");
                    table.ForeignKey(
                        name: "FK_Apply_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompt",
                        principalColumn: "PromptId");
                });

            migrationBuilder.CreateTable(
                name: "KnowLedge",
                columns: table => new
                {
                    KnowLedgeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    TeacherActivities = table.Column<string>(type: "text", nullable: false),
                    StudentActivities = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: true),
                    PromptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowLedge", x => x.KnowLedgeId);
                    table.ForeignKey(
                        name: "FK_KnowLedge_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId");
                    table.ForeignKey(
                        name: "FK_KnowLedge_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompt",
                        principalColumn: "PromptId");
                });

            migrationBuilder.CreateTable(
                name: "Practice",
                columns: table => new
                {
                    PracticeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    TeacherActivities = table.Column<string>(type: "text", nullable: false),
                    StudentActivities = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: true),
                    PromptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practice", x => x.PracticeId);
                    table.ForeignKey(
                        name: "FK_Practice_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId");
                    table.ForeignKey(
                        name: "FK_Practice_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompt",
                        principalColumn: "PromptId");
                });

            migrationBuilder.CreateTable(
                name: "StartUp",
                columns: table => new
                {
                    StartUpId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    TeacherActivities = table.Column<string>(type: "text", nullable: false),
                    StudentActivities = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: true),
                    PromptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartUp", x => x.StartUpId);
                    table.ForeignKey(
                        name: "FK_StartUp_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId");
                    table.ForeignKey(
                        name: "FK_StartUp_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompt",
                        principalColumn: "PromptId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apply_LessonId",
                table: "Apply",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apply_PromptId",
                table: "Apply",
                column: "PromptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KnowLedge_LessonId",
                table: "KnowLedge",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KnowLedge_PromptId",
                table: "KnowLedge",
                column: "PromptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Practice_LessonId",
                table: "Practice",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Practice_PromptId",
                table: "Practice",
                column: "PromptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StartUp_LessonId",
                table: "StartUp",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StartUp_PromptId",
                table: "StartUp",
                column: "PromptId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apply");

            migrationBuilder.DropTable(
                name: "KnowLedge");

            migrationBuilder.DropTable(
                name: "Practice");

            migrationBuilder.DropTable(
                name: "StartUp");

            migrationBuilder.RenameColumn(
                name: "SpecialAbility",
                table: "Lesson",
                newName: "StartUp");

            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "Lesson",
                newName: "Practice");

            migrationBuilder.RenameColumn(
                name: "GeneralCapacity",
                table: "Lesson",
                newName: "Knowledge");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "LessonPlan",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Lesson",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "Apply",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Lesson",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
