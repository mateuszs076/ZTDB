using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTDB.SQLDatabase.Migrations
{
    public partial class AddActualData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancelCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightDate = table.Column<DateTime>(nullable: false),
                    AirlineId = table.Column<int>(nullable: false),
                    OpCarrierFlightNumber = table.Column<int>(nullable: false),
                    OriginLocationId = table.Column<int>(nullable: true),
                    DestinationLocationId = table.Column<int>(nullable: true),
                    PlannedDepartureTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    ActualDepartureTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    DepartureDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    TaxiOut = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    TaxiIn = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    WheelsOn = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    WheelsOff = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    PlannedArrivalTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    ActualArrivalTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    ArrivalDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    CancelCodeId = table.Column<int>(nullable: false),
                    Diverted = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    PlannedElapsedTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    ActualElapsedTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    AirTime = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    Distance = table.Column<decimal>(type: "Numeric(18,2)", nullable: false),
                    CarrierDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: true),
                    WeatherDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: true),
                    NasDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: true),
                    SecurityDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: true),
                    LateAircraftDelay = table.Column<decimal>(type: "Numeric(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_CancelCode_CancelCodeId",
                        column: x => x.CancelCodeId,
                        principalTable: "CancelCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Locations_DestinationLocationId",
                        column: x => x.DestinationLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flights_Locations_OriginLocationId",
                        column: x => x.OriginLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CancelCodeId",
                table: "Flights",
                column: "CancelCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationLocationId",
                table: "Flights",
                column: "DestinationLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OriginLocationId",
                table: "Flights",
                column: "OriginLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "CancelCode");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
