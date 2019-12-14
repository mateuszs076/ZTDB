using Microsoft.EntityFrameworkCore.Migrations;

namespace ZTDB.SQLDatabase.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataToImport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FL_DATE = table.Column<string>(nullable: true),
                    OP_CARRIER = table.Column<string>(nullable: true),
                    OP_CARRIER_FL_NUM = table.Column<string>(nullable: true),
                    ORIGIN = table.Column<string>(nullable: true),
                    DEST = table.Column<string>(nullable: true),
                    CRS_DEP_TIME = table.Column<string>(nullable: true),
                    DEP_TIME = table.Column<string>(nullable: true),
                    DEP_DELAY = table.Column<string>(nullable: true),
                    TAXI_OUT = table.Column<string>(nullable: true),
                    WHEELS_OFF = table.Column<string>(nullable: true),
                    TAXI_IN = table.Column<string>(nullable: true),
                    WHEELS_ON = table.Column<string>(nullable: true),
                    CRS_ARR_TIME = table.Column<string>(nullable: true),
                    ARR_TIME = table.Column<string>(nullable: true),
                    ARR_DELAY = table.Column<string>(nullable: true),
                    CANCELLED = table.Column<string>(nullable: true),
                    CANCELLATION_CODE = table.Column<string>(nullable: true),
                    DIVERTED = table.Column<string>(nullable: true),
                    CRS_ELAPSED_TIME = table.Column<string>(nullable: true),
                    ACTUAL_ELAPSED_TIME = table.Column<string>(nullable: true),
                    AIR_TIME = table.Column<string>(nullable: true),
                    DISTANCE = table.Column<string>(nullable: true),
                    CARRIER_DELAY = table.Column<string>(nullable: true),
                    WEATHER_DELAY = table.Column<string>(nullable: true),
                    NAS_DELAY = table.Column<string>(nullable: true),
                    SECURITY_DELAY = table.Column<string>(nullable: true),
                    LATE_AIRCRAFT_DELAY = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToImport", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataToImport");
        }
    }
}
