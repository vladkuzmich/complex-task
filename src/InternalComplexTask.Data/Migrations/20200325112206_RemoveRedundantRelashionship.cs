using Microsoft.EntityFrameworkCore.Migrations;

namespace InternalComplexTask.Data.Migrations
{
    public partial class RemoveRedundantRelashionship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentId",
                table: "Projects",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
