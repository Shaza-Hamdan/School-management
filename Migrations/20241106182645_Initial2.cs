using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HwT_subjectNa_subjectId",
                table: "HwT");

            migrationBuilder.RenameColumn(
                name: "subjectId",
                table: "HwT",
                newName: "subjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_HwT_subjectId",
                table: "HwT",
                newName: "IX_HwT_subjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_HwT_subjectNa_subjectsId",
                table: "HwT",
                column: "subjectsId",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HwT_subjectNa_subjectsId",
                table: "HwT");

            migrationBuilder.RenameColumn(
                name: "subjectsId",
                table: "HwT",
                newName: "subjectId");

            migrationBuilder.RenameIndex(
                name: "IX_HwT_subjectsId",
                table: "HwT",
                newName: "IX_HwT_subjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_HwT_subjectNa_subjectId",
                table: "HwT",
                column: "subjectId",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
