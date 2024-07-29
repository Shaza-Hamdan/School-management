using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "perInfoId",
                table: "subjectNa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa",
                column: "perInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_subjectNa_Person_Information_perInfoId",
                table: "subjectNa",
                column: "perInfoId",
                principalTable: "Person_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjectNa_Person_Information_perInfoId",
                table: "subjectNa");

            migrationBuilder.DropIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa");

            migrationBuilder.DropColumn(
                name: "perInfoId",
                table: "subjectNa");
        }
    }
}
