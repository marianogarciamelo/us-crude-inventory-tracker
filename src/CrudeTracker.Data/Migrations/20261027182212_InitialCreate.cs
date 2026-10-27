using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudeTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "series",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    units = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_series", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "observations",
                columns: table => new
                {
                    series_code = table.Column<string>(type: "text", nullable: false),
                    period = table.Column<DateOnly>(type: "date", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_observations", x => new { x.series_code, x.period });
                    table.ForeignKey(
                        name: "fk_observations_series_series_code",
                        column: x => x.series_code,
                        principalTable: "series",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "observations");

            migrationBuilder.DropTable(
                name: "series");
        }
    }
}
