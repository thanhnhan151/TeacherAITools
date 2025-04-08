using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherAITools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordOtp",
                table: "User",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordOtp",
                table: "User");
        }
    }
}
