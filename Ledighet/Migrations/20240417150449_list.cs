using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ledighet.Migrations
{
    /// <inheritdoc />
    public partial class list : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "LeaveLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveLists_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveLists_LeaveApplications_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalTable: "LeaveApplications",
                        principalColumn: "LeaveApplicationId",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_LeaveLists_EmployeeId",
                table: "LeaveLists",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveLists_LeaveApplicationId",
                table: "LeaveLists",
                column: "LeaveApplicationId");
        }

    }
}
