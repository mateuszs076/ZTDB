using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTDB.SQLDatabase.Migrations
{
    public partial class ChangesInFlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WheelsOn",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "WheelsOff",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxiOut",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxiIn",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DepartureDelay",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ArrivalDelay",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualDepartureTime",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualArrivalTime",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WheelsOn",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WheelsOff",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxiOut",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxiIn",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DepartureDelay",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ArrivalDelay",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualDepartureTime",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualArrivalTime",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);
        }
    }
}
