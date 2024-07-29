using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa");

            migrationBuilder.AddColumn<int>(
                name: "perInfoID",
                table: "marks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "subjectsID",
                table: "marks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "subjectsId",
                table: "HwT",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "homeworkTId",
                table: "HwS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "perInfoId",
                table: "HwS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa",
                column: "perInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_marks_perInfoID",
                table: "marks",
                column: "perInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_marks_subjectsID",
                table: "marks",
                column: "subjectsID");

            migrationBuilder.CreateIndex(
                name: "IX_HwT_subjectsId",
                table: "HwT",
                column: "subjectsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HwS_homeworkTId",
                table: "HwS",
                column: "homeworkTId");

            migrationBuilder.CreateIndex(
                name: "IX_HwS_perInfoId",
                table: "HwS",
                column: "perInfoId");

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
                name: "FK_marks_Person_Information_perInfoID",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_subjectNa_subjectsID",
                table: "marks");

            migrationBuilder.DropIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa");

            migrationBuilder.DropIndex(
                name: "IX_marks_perInfoID",
                table: "marks");

            migrationBuilder.DropIndex(
                name: "IX_marks_subjectsID",
                table: "marks");

            migrationBuilder.DropIndex(
                name: "IX_HwT_subjectsId",
                table: "HwT");

            migrationBuilder.DropIndex(
                name: "IX_HwS_homeworkTId",
                table: "HwS");

            migrationBuilder.DropIndex(
                name: "IX_HwS_perInfoId",
                table: "HwS");

            migrationBuilder.DropColumn(
                name: "perInfoID",
                table: "marks");

            migrationBuilder.DropColumn(
                name: "subjectsID",
                table: "marks");

            migrationBuilder.DropColumn(
                name: "subjectsId",
                table: "HwT");

            migrationBuilder.DropColumn(
                name: "homeworkTId",
                table: "HwS");

            migrationBuilder.DropColumn(
                name: "perInfoId",
                table: "HwS");

            migrationBuilder.CreateIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa",
                column: "perInfoId");
        }
    }
}
