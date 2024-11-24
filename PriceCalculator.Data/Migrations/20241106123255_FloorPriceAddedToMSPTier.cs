using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceCalculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class FloorPriceAddedToMSPTier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorPrice",
                table: "MSPTiers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorPrice",
                table: "MSPTiers");
        }
    }
}
