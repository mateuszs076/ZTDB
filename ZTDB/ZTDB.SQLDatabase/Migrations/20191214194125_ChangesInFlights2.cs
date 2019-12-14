using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTDB.SQLDatabase.Migrations
{
    public partial class ChangesInFlights2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Distance",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AirTime",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Distance",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AirTime",
                table: "Flights",
                type: "Numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Numeric(18,2)",
                oldNullable: true);
        }
    }
}
