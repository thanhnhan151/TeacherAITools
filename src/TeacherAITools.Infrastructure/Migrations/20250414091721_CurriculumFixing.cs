using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CurriculumFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Curriculum",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Curriculum",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CurriculumActivity",
                columns: table => new
                {
                    CurriculumActivityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurriculumAcitityDescription = table.Column<string>(type: "text", nullable: false),
                    CurriculumId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumActivity", x => x.CurriculumActivityId);
                    table.ForeignKey(
                        name: "FK_CurriculumActivity_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumTopic",
                columns: table => new
                {
                    CurriculumTopicId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurriculumTopicName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumTopic", x => x.CurriculumTopicId);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumSection",
                columns: table => new
                {
                    CurriculumSectionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurriculumSectionName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CurriculumTopicId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSection", x => x.CurriculumSectionId);
                    table.ForeignKey(
                        name: "FK_CurriculumSection_CurriculumTopic_CurriculumTopicId",
                        column: x => x.CurriculumTopicId,
                        principalTable: "CurriculumTopic",
                        principalColumn: "CurriculumTopicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumSubSection",
                columns: table => new
                {
                    CurriculumSubSectionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurriculumSubSectionName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CurriculumSectionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSubSection", x => x.CurriculumSubSectionId);
                    table.ForeignKey(
                        name: "FK_CurriculumSubSection_CurriculumSection_CurriculumSectionId",
                        column: x => x.CurriculumSectionId,
                        principalTable: "CurriculumSection",
                        principalColumn: "CurriculumSectionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumDetail",
                columns: table => new
                {
                    CurriculumDetailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurriculumContent = table.Column<string>(type: "text", nullable: false),
                    CurriculumGoal = table.Column<string>(type: "text", nullable: false),
                    CurriculumId = table.Column<int>(type: "integer", nullable: true),
                    CurriculumSubSectionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumDetail", x => x.CurriculumDetailId);
                    table.ForeignKey(
                        name: "FK_CurriculumDetail_CurriculumSubSection_CurriculumSubSectionId",
                        column: x => x.CurriculumSubSectionId,
                        principalTable: "CurriculumSubSection",
                        principalColumn: "CurriculumSubSectionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CurriculumDetail_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "CurriculumId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_GradeId",
                table: "Curriculum",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumActivity_CurriculumId",
                table: "CurriculumActivity",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumDetail_CurriculumId",
                table: "CurriculumDetail",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumDetail_CurriculumSubSectionId",
                table: "CurriculumDetail",
                column: "CurriculumSubSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSection_CurriculumTopicId",
                table: "CurriculumSection",
                column: "CurriculumTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubSection_CurriculumSectionId",
                table: "CurriculumSubSection",
                column: "CurriculumSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculum_Grade_GradeId",
                table: "Curriculum",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Grade_GradeId",
                table: "Curriculum");

            migrationBuilder.DropTable(
                name: "CurriculumActivity");

            migrationBuilder.DropTable(
                name: "CurriculumDetail");

            migrationBuilder.DropTable(
                name: "CurriculumSubSection");

            migrationBuilder.DropTable(
                name: "CurriculumSection");

            migrationBuilder.DropTable(
                name: "CurriculumTopic");

            migrationBuilder.DropIndex(
                name: "IX_Curriculum_GradeId",
                table: "Curriculum");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Curriculum");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Curriculum",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);
        }
    }
}
