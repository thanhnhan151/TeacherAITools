﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LessonFixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_Lesson_LessonId",
                table: "Prompt");

            migrationBuilder.DropIndex(
                name: "IX_Prompt_LessonId",
                table: "Prompt");

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_LessonId",
                table: "Prompt",
                column: "LessonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_Lesson_LessonId",
                table: "Prompt",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_Lesson_LessonId",
                table: "Prompt");

            migrationBuilder.DropIndex(
                name: "IX_Prompt_LessonId",
                table: "Prompt");

            migrationBuilder.CreateIndex(
                name: "IX_Prompt_LessonId",
                table: "Prompt",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_Lesson_LessonId",
                table: "Prompt",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
