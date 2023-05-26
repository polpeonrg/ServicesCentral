using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesCentral.Migrations
{
    /// <inheritdoc />
    public partial class removeReserveDetailMarket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveDetail");

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "Reservation");

            migrationBuilder.CreateTable(
                name: "ReserveDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketID = table.Column<int>(type: "int", nullable: false),
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveDetail", x => x.Id);
                });
        }
    }
}
