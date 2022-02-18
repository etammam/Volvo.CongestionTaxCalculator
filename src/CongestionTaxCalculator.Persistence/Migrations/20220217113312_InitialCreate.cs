using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CongestionTaxCalculator.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ignores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Month = table.Column<string>(type: "TEXT", nullable: true),
                    DaysBeforeHoliday = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ignores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ignores_Cities_Id",
                        column: x => x.Id,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Start = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    End = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    RateValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    CityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleId = table.Column<string>(type: "TEXT", nullable: false),
                    TaxCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Issued = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxHistories_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Issued = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    VehicleId = table.Column<string>(type: "TEXT", nullable: true),
                    CityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxPayments_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), 1, "Gothenburg" });

            migrationBuilder.InsertData(
                table: "Ignores",
                columns: new[] { "Id", "DaysBeforeHoliday", "Month" },
                values: new object[] { new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), 1, "[\"July\"]" });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("02c23596-fda9-42e8-9a6e-40fe59a86a1d"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(17, 59, 0), 13m, new TimeOnly(17, 0, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("0842e0e6-c6d0-46a5-a3ec-294fe3a743bb"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(7, 59, 0), 18m, new TimeOnly(7, 0, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("0d9fb9c8-1f74-44b2-b8ae-287235dc5af1"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(6, 29, 0), 8m, new TimeOnly(6, 0, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("12858360-07a6-4bea-9a51-0dcb42667577"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(14, 59, 0), 8m, new TimeOnly(8, 30, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("167926a7-4fd1-403a-a0c4-b053a3498dfe"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(5, 59, 0), 0m, new TimeOnly(18, 30, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("1c9c1143-0ad8-4f8e-b9c9-bdb65df92de5"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(6, 59, 0), 13m, new TimeOnly(6, 30, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("40369fa8-313b-4acf-99be-d3516f36f2b1"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(16, 59, 0), 18m, new TimeOnly(15, 30, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("87b12225-9606-415e-a447-1067a6d37cac"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(8, 29, 0), 13m, new TimeOnly(8, 0, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("b61927b8-cc15-4732-8d87-dda1f5d9c9cc"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(18, 29, 0), 8m, new TimeOnly(18, 0, 0) });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "CityId", "End", "RateValue", "Start" },
                values: new object[] { new Guid("eb43daa0-9026-4821-92fd-8ee42dfbacf3"), new Guid("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new TimeOnly(15, 29, 0), 13m, new TimeOnly(15, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CityId",
                table: "Rates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxHistories_CityId",
                table: "TaxHistories",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPayments_CityId",
                table: "TaxPayments",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ignores");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "TaxHistories");

            migrationBuilder.DropTable(
                name: "TaxPayments");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
