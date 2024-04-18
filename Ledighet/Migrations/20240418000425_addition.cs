using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ledighet.Migrations
{
    /// <inheritdoc />
    public partial class addition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveApplicationNote",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveApplicationNote",
                table: "LeaveApplications");
        }
    }
}
