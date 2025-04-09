using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Clear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Requirement_RequirementId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_SchoolSupply_SchoolSupplyId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonHistory_PeriodDetail_PeriodDetailId",
                table: "LessonHistory");

            migrationBuilder.DropTable(
                name: "PeriodDetail");

            migrationBuilder.DropTable(
                name: "Requirement");

            migrationBuilder.DropTable(
                name: "SchoolSupply");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropIndex(
                name: "IX_LessonHistory_PeriodDetailId",
                table: "LessonHistory");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_RequirementId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_SchoolSupplyId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "PeriodDetailId",
                table: "LessonHistory");

            migrationBuilder.DropColumn(
                name: "RequirementId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "SchoolSupplyId",
                table: "Lesson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodDetailId",
                table: "LessonHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequirementId",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolSupplyId",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Period_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Period_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    RequirementId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.RequirementId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSupply",
                columns: table => new
                {
                    SchoolSupplyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSupply", x => x.SchoolSupplyId);
                });

            migrationBuilder.CreateTable(
                name: "PeriodDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PeriodId = table.Column<int>(type: "integer", nullable: false),
                    Apply = table.Column<string>(type: "text", nullable: false),
                    Knowledge = table.Column<string>(type: "text", nullable: false),
                    Practice = table.Column<string>(type: "text", nullable: false),
                    StartUp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodDetail_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonHistory_PeriodDetailId",
                table: "LessonHistory",
                column: "PeriodDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_RequirementId",
                table: "Lesson",
                column: "RequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_SchoolSupplyId",
                table: "Lesson",
                column: "SchoolSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Period_LessonId",
                table: "Period",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Period_UserId",
                table: "Period",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDetail_PeriodId",
                table: "PeriodDetail",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Requirement_RequirementId",
                table: "Lesson",
                column: "RequirementId",
                principalTable: "Requirement",
                principalColumn: "RequirementId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_SchoolSupply_SchoolSupplyId",
                table: "Lesson",
                column: "SchoolSupplyId",
                principalTable: "SchoolSupply",
                principalColumn: "SchoolSupplyId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonHistory_PeriodDetail_PeriodDetailId",
                table: "LessonHistory",
                column: "PeriodDetailId",
                principalTable: "PeriodDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
