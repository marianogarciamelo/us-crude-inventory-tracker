using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudeTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "series",
                columns: new[] { "code", "description", "units" },
                values: new object[] { "WCESTUS1", "U.S. Ending Stocks excluding SPR of Crude Oil", "Thousand Barrels" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "series",
                keyColumn: "code",
                keyValue: "WCESTUS1");
        }
    }
}
