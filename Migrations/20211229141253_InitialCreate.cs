using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VouchersBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voucher_type",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "webshop",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_webshop", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voucher",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    valid_from = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valid_to = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    likes = table.Column<int>(type: "integer", nullable: false),
                    dislikes = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<long>(type: "bigint", nullable: false),
                    unit_id = table.Column<long>(type: "bigint", nullable: false),
                    webshop_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher", x => x.id);
                    table.ForeignKey(
                        name: "voucher_unit_FK",
                        column: x => x.unit_id,
                        principalTable: "unit",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_voucher_type_FK",
                        column: x => x.type_id,
                        principalTable: "voucher_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_webshop_FK",
                        column: x => x.webshop_id,
                        principalTable: "webshop",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_voucher_type_id",
                table: "voucher",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_voucher_unit_id",
                table: "voucher",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_voucher_webshop_id",
                table: "voucher",
                column: "webshop_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voucher");

            migrationBuilder.DropTable(
                name: "unit");

            migrationBuilder.DropTable(
                name: "voucher_type");

            migrationBuilder.DropTable(
                name: "webshop");
        }
    }
}
