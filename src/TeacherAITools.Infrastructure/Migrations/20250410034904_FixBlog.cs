using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Lesson_LessonId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "DisapprovedReason",
                table: "Lesson");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Blog",
                newName: "TeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_LessonId",
                table: "Blog",
                newName: "IX_Blog_TeacherLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_TeacherLesson_TeacherLessonId",
                table: "Blog",
                column: "TeacherLessonId",
                principalTable: "TeacherLesson",
                principalColumn: "TeacherLessonId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_TeacherLesson_TeacherLessonId",
                table: "Blog");

            migrationBuilder.RenameColumn(
                name: "TeacherLessonId",
                table: "Blog",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_TeacherLessonId",
                table: "Blog",
                newName: "IX_Blog_LessonId");

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReason",
                table: "Lesson",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Lesson_LessonId",
                table: "Blog",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
