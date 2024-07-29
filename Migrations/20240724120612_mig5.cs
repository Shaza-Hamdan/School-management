using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPassword",
                table: "registrations");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "registrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiration",
                table: "registrations",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "registrations");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiration",
                table: "registrations");

            migrationBuilder.AddColumn<string>(
                name: "OldPassword",
                table: "registrations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
