using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interfaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Short = table.Column<string>(type: "TEXT", nullable: false),
                    HexColor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "objects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogueInterfaceCatalogueObject",
                columns: table => new
                {
                    InterfacesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ObjectsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueInterfaceCatalogueObject", x => new { x.InterfacesId, x.ObjectsId });
                    table.ForeignKey(
                        name: "FK_CatalogueInterfaceCatalogueObject_interfaces_InterfacesId",
                        column: x => x.InterfacesId,
                        principalTable: "interfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogueInterfaceCatalogueObject_objects_ObjectsId",
                        column: x => x.ObjectsId,
                        principalTable: "objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogueInterfaceCatalogueObject_ObjectsId",
                table: "CatalogueInterfaceCatalogueObject",
                column: "ObjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogueInterfaceCatalogueObject");

            migrationBuilder.DropTable(
                name: "interfaces");

            migrationBuilder.DropTable(
                name: "objects");
        }
    }
}
