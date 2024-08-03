using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class mig13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HwS_HwT_homeworkTId",
                table: "HwS");

            migrationBuilder.DropForeignKey(
                name: "FK_HwS_Person_Information_perInfoId",
                table: "HwS");

            migrationBuilder.DropForeignKey(
                name: "FK_HwT_subjectNa_subjectsId",
                table: "HwT");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_Person_Information_perInfoID",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_subjectNa_subjectsID",
                table: "marks");

            migrationBuilder.RenameColumn(
                name: "subjectsID",
                table: "marks",
                newName: "subjectsId");

            migrationBuilder.RenameColumn(
                name: "perInfoID",
                table: "marks",
                newName: "perInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_marks_subjectsID",
                table: "marks",
                newName: "IX_marks_subjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_marks_perInfoID",
                table: "marks",
                newName: "IX_marks_perInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HwS_HwT_homeworkTId",
                table: "HwS",
                column: "homeworkTId",
                principalTable: "HwT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HwS_Person_Information_perInfoId",
                table: "HwS",
                column: "perInfoId",
                principalTable: "Person_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HwT_subjectNa_subjectsId",
                table: "HwT",
                column: "subjectsId",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_marks_Person_Information_perInfoId",
                table: "marks",
                column: "perInfoId",
                principalTable: "Person_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_marks_subjectNa_subjectsId",
                table: "marks",
                column: "subjectsId",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HwS_HwT_homeworkTId",
                table: "HwS");

            migrationBuilder.DropForeignKey(
                name: "FK_HwS_Person_Information_perInfoId",
                table: "HwS");

            migrationBuilder.DropForeignKey(
                name: "FK_HwT_subjectNa_subjectsId",
                table: "HwT");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_Person_Information_perInfoId",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_subjectNa_subjectsId",
                table: "marks");

            migrationBuilder.RenameColumn(
                name: "subjectsId",
                table: "marks",
                newName: "subjectsID");

            migrationBuilder.RenameColumn(
                name: "perInfoId",
                table: "marks",
                newName: "perInfoID");

            migrationBuilder.RenameIndex(
                name: "IX_marks_subjectsId",
                table: "marks",
                newName: "IX_marks_subjectsID");

            migrationBuilder.RenameIndex(
                name: "IX_marks_perInfoId",
                table: "marks",
                newName: "IX_marks_perInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_HwS_HwT_homeworkTId",
                table: "HwS",
                column: "homeworkTId",
                principalTable: "HwT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HwS_Person_Information_perInfoId",
                table: "HwS",
                column: "perInfoId",
                principalTable: "Person_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HwT_subjectNa_subjectsId",
                table: "HwT",
                column: "subjectsId",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_marks_Person_Information_perInfoID",
                table: "marks",
                column: "perInfoID",
                principalTable: "Person_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_marks_subjectNa_subjectsID",
                table: "marks",
                column: "subjectsID",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
