using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixQuizAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswer_QuizQuestion_AnswerId",
                table: "QuizAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "QuizAnswer",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswer_QuestionId",
                table: "QuizAnswer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswer_QuizQuestion_QuestionId",
                table: "QuizAnswer",
                column: "QuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswer_QuizQuestion_QuestionId",
                table: "QuizAnswer");

            migrationBuilder.DropIndex(
                name: "IX_QuizAnswer_QuestionId",
                table: "QuizAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "QuizAnswer",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswer_QuizQuestion_AnswerId",
                table: "QuizAnswer",
                column: "AnswerId",
                principalTable: "QuizQuestion",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
