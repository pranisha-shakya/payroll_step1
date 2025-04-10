using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payroll_step1.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Leaves",
                newName: "LeaveType");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Leaves",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Employees",
                newName: "Designation");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Leaves",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Leaves");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Leaves",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "LeaveType",
                table: "Leaves",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "Employees",
                newName: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
