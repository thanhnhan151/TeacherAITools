using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_TeacherLesson_TeacherLessonId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonHistory_TeacherLesson_TeacherLessonId",
                table: "LessonHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLesson_Prompt_PromptId",
                table: "TeacherLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLesson_User_UserId",
                table: "TeacherLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherLesson",
                table: "TeacherLesson");

            migrationBuilder.RenameTable(
                name: "TeacherLesson",
                newName: "LessonPlan");

            migrationBuilder.RenameColumn(
                name: "TeacherLessonId",
                table: "LessonHistory",
                newName: "LessonPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonHistory_TeacherLessonId",
                table: "LessonHistory",
                newName: "IX_LessonHistory_LessonPlanId");

            migrationBuilder.RenameColumn(
                name: "TeacherLessonId",
                table: "Blog",
                newName: "LessonPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_TeacherLessonId",
                table: "Blog",
                newName: "IX_Blog_LessonPlanId");

            migrationBuilder.RenameColumn(
                name: "TeacherLessonId",
                table: "LessonPlan",
                newName: "LessonPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherLesson_UserId",
                table: "LessonPlan",
                newName: "IX_LessonPlan_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherLesson_PromptId",
                table: "LessonPlan",
                newName: "IX_LessonPlan_PromptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonPlan",
                table: "LessonPlan",
                column: "LessonPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_LessonPlan_LessonPlanId",
                table: "Blog",
                column: "LessonPlanId",
                principalTable: "LessonPlan",
                principalColumn: "LessonPlanId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonHistory_LessonPlan_LessonPlanId",
                table: "LessonHistory",
                column: "LessonPlanId",
                principalTable: "LessonPlan",
                principalColumn: "LessonPlanId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonPlan_Prompt_PromptId",
                table: "LessonPlan",
                column: "PromptId",
                principalTable: "Prompt",
                principalColumn: "PromptId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonPlan_User_UserId",
                table: "LessonPlan",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_LessonPlan_LessonPlanId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonHistory_LessonPlan_LessonPlanId",
                table: "LessonHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonPlan_Prompt_PromptId",
                table: "LessonPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonPlan_User_UserId",
                table: "LessonPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonPlan",
                table: "LessonPlan");

            migrationBuilder.RenameTable(
                name: "LessonPlan",
                newName: "TeacherLesson");

            migrationBuilder.RenameColumn(
                name: "LessonPlanId",
                table: "LessonHistory",
                newName: "TeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonHistory_LessonPlanId",
                table: "LessonHistory",
                newName: "IX_LessonHistory_TeacherLessonId");

            migrationBuilder.RenameColumn(
                name: "LessonPlanId",
                table: "Blog",
                newName: "TeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_LessonPlanId",
                table: "Blog",
                newName: "IX_Blog_TeacherLessonId");

            migrationBuilder.RenameColumn(
                name: "LessonPlanId",
                table: "TeacherLesson",
                newName: "TeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonPlan_UserId",
                table: "TeacherLesson",
                newName: "IX_TeacherLesson_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonPlan_PromptId",
                table: "TeacherLesson",
                newName: "IX_TeacherLesson_PromptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherLesson",
                table: "TeacherLesson",
                column: "TeacherLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_TeacherLesson_TeacherLessonId",
                table: "Blog",
                column: "TeacherLessonId",
                principalTable: "TeacherLesson",
                principalColumn: "TeacherLessonId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonHistory_TeacherLesson_TeacherLessonId",
                table: "LessonHistory",
                column: "TeacherLessonId",
                principalTable: "TeacherLesson",
                principalColumn: "TeacherLessonId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLesson_Prompt_PromptId",
                table: "TeacherLesson",
                column: "PromptId",
                principalTable: "Prompt",
                principalColumn: "PromptId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLesson_User_UserId",
                table: "TeacherLesson",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
