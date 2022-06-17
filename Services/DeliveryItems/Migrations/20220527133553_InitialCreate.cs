using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryItems.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Barcode = table.Column<string>(type: "text", nullable: false),
                    DeliveryPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    BagStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VolumetricWeight = table.Column<float>(type: "real", nullable: false),
                    PackageStatus = table.Column<int>(type: "integer", nullable: false),
                    DeliveryPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    Barcode = table.Column<string>(type: "text", nullable: false),
                    AssignedBagId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Bags_AssignedBagId",
                        column: x => x.AssignedBagId,
                        principalTable: "Bags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_AssignedBagId",
                table: "Packages",
                column: "AssignedBagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Bags");
        }
    }
}
