using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InterfaceModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Changes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interfaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Locked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Archived = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interface_assignments",
                columns: table => new
                {
                    ReferenceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interface_assignments", x => new { x.ReferenceId, x.DestinationId });
                    table.ForeignKey(
                        name: "FK_interface_assignments_interfaces_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "interfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_interface_assignments_interfaces_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "interfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interface_properties",
                columns: table => new
                {
                    ReferenceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interface_properties", x => new { x.ReferenceId, x.Name });
                    table.ForeignKey(
                        name: "FK_interface_properties_interfaces_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "interfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_history_EntityId",
                table: "history",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_interface_assignments_Deleted",
                table: "interface_assignments",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_interface_assignments_DestinationId",
                table: "interface_assignments",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_interface_properties_Deleted",
                table: "interface_properties",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_interfaces_Deleted",
                table: "interfaces",
                column: "Deleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "interface_assignments");

            migrationBuilder.DropTable(
                name: "interface_properties");

            migrationBuilder.DropTable(
                name: "interfaces");
        }
    }
}
