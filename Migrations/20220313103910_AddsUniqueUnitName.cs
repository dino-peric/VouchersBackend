using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VouchersBackend.Migrations
{
    public partial class AddsUniqueUnitName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_unit_name",
                table: "unit",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_unit_name",
                table: "unit");
        }
    }
}
