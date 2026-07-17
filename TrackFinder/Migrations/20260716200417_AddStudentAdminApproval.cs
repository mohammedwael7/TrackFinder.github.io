using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackFinder.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentAdminApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminApproved",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminApproved",
                table: "Students");
        }
    }
}
