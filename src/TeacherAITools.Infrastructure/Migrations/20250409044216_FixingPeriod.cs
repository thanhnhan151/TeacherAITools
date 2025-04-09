using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_User_UserId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_UserId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lesson");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Period",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Period",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Period_UserId",
                table: "Period",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Period_User_UserId",
                table: "Period",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Period_User_UserId",
                table: "Period");

            migrationBuilder.DropIndex(
                name: "IX_Period_UserId",
                table: "Period");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Period");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Period");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_UserId",
                table: "Lesson",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_User_UserId",
                table: "Lesson",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
