using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firebird.Embedded.AspNetCore.Migrations.Data
{
    public partial class InitStudentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "IsRegularStudent" },
                values: new object[] { "13f014fc-3c8b-43e8-b7ec-e4c12fc4294d", 25, false });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "IsRegularStudent" },
                values: new object[] { "becaf884-4f6a-4529-9612-2d07c4ff10fe", 30, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "13f014fc-3c8b-43e8-b7ec-e4c12fc4294d");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "becaf884-4f6a-4529-9612-2d07c4ff10fe");
        }
    }
}
