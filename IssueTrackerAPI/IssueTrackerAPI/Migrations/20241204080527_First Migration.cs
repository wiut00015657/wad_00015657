using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(512)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueDbSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueDbSet_EmployeeDbSet_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueDbSet_EmployeeId",
                table: "IssueDbSet",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueDbSet");

            migrationBuilder.DropTable(
                name: "EmployeeDbSet");
        }
    }
}
