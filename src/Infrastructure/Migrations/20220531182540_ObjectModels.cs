using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ObjectModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interface_properties_interfaces_ReferenceId",
                table: "interface_properties");

            migrationBuilder.DropTable(
                name: "interface_assignments");

            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "interface_properties",
                newName: "InterfaceId");

            migrationBuilder.AddColumn<Guid>(
                name: "CTObjectId",
                table: "interfaces",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "objects",
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
                    table.PrimaryKey("PK_objects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "object_properties",
                columns: table => new
                {
                    ObjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_object_properties", x => new { x.ObjectId, x.Name });
                    table.ForeignKey(
                        name: "FK_object_properties_objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_interfaces_CTObjectId",
                table: "interfaces",
                column: "CTObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_object_properties_Deleted",
                table: "object_properties",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_objects_Deleted",
                table: "objects",
                column: "Deleted");

            migrationBuilder.AddForeignKey(
                name: "FK_interface_properties_interfaces_InterfaceId",
                table: "interface_properties",
                column: "InterfaceId",
                principalTable: "interfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_interfaces_objects_CTObjectId",
                table: "interfaces",
                column: "CTObjectId",
                principalTable: "objects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interface_properties_interfaces_InterfaceId",
                table: "interface_properties");

            migrationBuilder.DropForeignKey(
                name: "FK_interfaces_objects_CTObjectId",
                table: "interfaces");

            migrationBuilder.DropTable(
                name: "object_properties");

            migrationBuilder.DropTable(
                name: "objects");

            migrationBuilder.DropIndex(
                name: "IX_interfaces_CTObjectId",
                table: "interfaces");

            migrationBuilder.DropColumn(
                name: "CTObjectId",
                table: "interfaces");

            migrationBuilder.RenameColumn(
                name: "InterfaceId",
                table: "interface_properties",
                newName: "ReferenceId");

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

            migrationBuilder.CreateIndex(
                name: "IX_interface_assignments_Deleted",
                table: "interface_assignments",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_interface_assignments_DestinationId",
                table: "interface_assignments",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_interface_properties_interfaces_ReferenceId",
                table: "interface_properties",
                column: "ReferenceId",
                principalTable: "interfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
