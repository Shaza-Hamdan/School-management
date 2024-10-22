using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial.Migrations
{
    /// <inheritdoc />
    public partial class InitialMySQLMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HwS_Person_Information_PerInfoId",
                table: "HwS");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_Person_Information_perInfoId",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_subjectNa_subjectsId",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_subjectNa_Person_Information_perInfoId",
                table: "subjectNa");

            migrationBuilder.DropTable(
                name: "Person_Information");

            migrationBuilder.RenameColumn(
                name: "perInfoId",
                table: "subjectNa",
                newName: "RegistrationId");

            migrationBuilder.RenameIndex(
                name: "IX_subjectNa_perInfoId",
                table: "subjectNa",
                newName: "IX_subjectNa_RegistrationId");

            migrationBuilder.RenameColumn(
                name: "perInfoId",
                table: "marks",
                newName: "RegistrationId");

            migrationBuilder.RenameIndex(
                name: "IX_marks_perInfoId",
                table: "marks",
                newName: "IX_marks_RegistrationId");

            migrationBuilder.RenameColumn(
                name: "PerInfoId",
                table: "HwS",
                newName: "RegistrationId");

            migrationBuilder.RenameIndex(
                name: "IX_HwS_PerInfoId",
                table: "HwS",
                newName: "IX_HwS_RegistrationId");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "registrations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_HwS_registrations_RegistrationId",
                table: "HwS",
                column: "RegistrationId",
                principalTable: "registrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_marks_registrations_RegistrationId",
                table: "marks",
                column: "RegistrationId",
                principalTable: "registrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_marks_subjectNa_subjectsId",
                table: "marks",
                column: "subjectsId",
                principalTable: "subjectNa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subjectNa_registrations_RegistrationId",
                table: "subjectNa",
                column: "RegistrationId",
                principalTable: "registrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HwS_registrations_RegistrationId",
                table: "HwS");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_registrations_RegistrationId",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_marks_subjectNa_subjectsId",
                table: "marks");

            migrationBuilder.DropForeignKey(
                name: "FK_subjectNa_registrations_RegistrationId",
                table: "subjectNa");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "registrations");

            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "subjectNa",
                newName: "perInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_subjectNa_RegistrationId",
                table: "subjectNa",
                newName: "IX_subjectNa_perInfoId");

            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "marks",
                newName: "perInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_marks_RegistrationId",
                table: "marks",
                newName: "IX_marks_perInfoId");

            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "HwS",
                newName: "PerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_HwS_RegistrationId",
                table: "HwS",
                newName: "IX_HwS_PerInfoId");

            migrationBuilder.CreateTable(
                name: "Person_Information",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Person = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonalNum = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person_Information", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_HwS_Person_Information_PerInfoId",
                table: "HwS",
                column: "PerInfoId",
                principalTable: "Person_Information",
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

            migrationBuilder.AddForeignKey(
                name: "FK_subjectNa_Person_Information_perInfoId",
                table: "subjectNa",
                column: "perInfoId",
                principalTable: "Person_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
