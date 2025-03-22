using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Grade_GradeId",
                table: "Curriculum");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_TeachingTool_TeachingToolId",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "TeachingTool");

            migrationBuilder.DropIndex(
                name: "IX_Curriculum_GradeId",
                table: "Curriculum");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Curriculum");

            migrationBuilder.RenameColumn(
                name: "TeachingToolId",
                table: "Lesson",
                newName: "SchoolSupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_TeachingToolId",
                table: "Lesson",
                newName: "IX_Lesson_SchoolSupplyId");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Prompt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Module",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Module",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Blog",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BookNumber = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
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

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_LessonId",
                table: "Prompt",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_BookId",
                table: "Module",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_GradeId",
                table: "Module",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CategoryId",
                table: "Blog",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Category_CategoryId",
                table: "Blog",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_SchoolSupply_SchoolSupplyId",
                table: "Lesson",
                column: "SchoolSupplyId",
                principalTable: "SchoolSupply",
                principalColumn: "SchoolSupplyId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Book_BookId",
                table: "Module",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Grade_GradeId",
                table: "Module",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_Lesson_LessonId",
                table: "Prompt",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Category_CategoryId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_SchoolSupply_SchoolSupplyId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Module_Book_BookId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_Module_Grade_GradeId",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_Lesson_LessonId",
                table: "Prompt");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "SchoolSupply");

            migrationBuilder.DropIndex(
                name: "IX_Prompt_LessonId",
                table: "Prompt");

            migrationBuilder.DropIndex(
                name: "IX_Module_BookId",
                table: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Module_GradeId",
                table: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Blog_CategoryId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Prompt");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Blog");

            migrationBuilder.RenameColumn(
                name: "SchoolSupplyId",
                table: "Lesson",
                newName: "TeachingToolId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_SchoolSupplyId",
                table: "Lesson",
                newName: "IX_Lesson_TeachingToolId");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Curriculum",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TeachingTool",
                columns: table => new
                {
                    TeachingToolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingTool", x => x.TeachingToolId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_GradeId",
                table: "Curriculum",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculum_Grade_GradeId",
                table: "Curriculum",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_TeachingTool_TeachingToolId",
                table: "Lesson",
                column: "TeachingToolId",
                principalTable: "TeachingTool",
                principalColumn: "TeachingToolId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
