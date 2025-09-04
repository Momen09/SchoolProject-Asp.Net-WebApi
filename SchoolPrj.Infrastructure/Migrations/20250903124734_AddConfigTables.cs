using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolPrj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_studentSubjects_SubID",
                table: "studentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departmentSubjects",
                table: "departmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_departmentSubjects_SubID",
                table: "departmentSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "studentSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "departmentSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DNameEn",
                table: "departments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "departments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "InsManager",
                table: "departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects",
                columns: new[] { "SubID", "StudID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_departmentSubjects",
                table: "departmentSubjects",
                columns: new[] { "SubID", "DID" });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ENameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructor_Instructor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructor",
                        principalColumn: "InsId");
                    table.ForeignKey(
                        name: "FK_Instructor_departments_DID",
                        column: x => x.DID,
                        principalTable: "departments",
                        principalColumn: "DID");
                });

            migrationBuilder.CreateTable(
                name: "InsSubject",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsSubject", x => new { x.SubId, x.InsId });
                    table.ForeignKey(
                        name: "FK_InsSubject_Instructor_InsId",
                        column: x => x.InsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsSubject_subjects_SubId",
                        column: x => x.SubId,
                        principalTable: "subjects",
                        principalColumn: "SubId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_InsManager",
                table: "departments",
                column: "InsManager",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsSubject_InsId",
                table: "InsSubject",
                column: "InsId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DID",
                table: "Instructor",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_Instructor_InsManager",
                table: "departments",
                column: "InsManager",
                principalTable: "Instructor",
                principalColumn: "InsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_Instructor_InsManager",
                table: "departments");

            migrationBuilder.DropTable(
                name: "InsSubject");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departmentSubjects",
                table: "departmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_departments_InsManager",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "InsManager",
                table: "departments");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "studentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "departmentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "DNameEn",
                table: "departments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departmentSubjects",
                table: "departmentSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjects_SubID",
                table: "studentSubjects",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_departmentSubjects_SubID",
                table: "departmentSubjects",
                column: "SubID");
        }
    }
}
