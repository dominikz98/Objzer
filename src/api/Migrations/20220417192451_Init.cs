using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Entity = table.Column<string>(type: "TEXT", nullable: false),
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Changes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterfaceDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinimalDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    InterfaceDTOId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinimalDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinimalDTO_InterfaceDTO_InterfaceDTOId",
                        column: x => x.InterfaceDTOId,
                        principalTable: "InterfaceDTO",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "interface_assignments",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interface_assignments", x => x.ParentId);
                });

            migrationBuilder.CreateTable(
                name: "interfaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CTInterfaceAssignmentParentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interfaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interfaces_interface_assignments_CTInterfaceAssignmentParentId",
                        column: x => x.CTInterfaceAssignmentParentId,
                        principalTable: "interface_assignments",
                        principalColumn: "ParentId");
                });

            migrationBuilder.CreateTable(
                name: "interface_properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    InterfaceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interface_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interface_properties_interfaces_InterfaceId",
                        column: x => x.InterfaceId,
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
                name: "IX_interface_properties_Deleted",
                table: "interface_properties",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_interface_properties_InterfaceId",
                table: "interface_properties",
                column: "InterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_interfaces_CTInterfaceAssignmentParentId",
                table: "interfaces",
                column: "CTInterfaceAssignmentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_interfaces_Deleted",
                table: "interfaces",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_MinimalDTO_InterfaceDTOId",
                table: "MinimalDTO",
                column: "InterfaceDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_interface_assignments_interfaces_ParentId",
                table: "interface_assignments",
                column: "ParentId",
                principalTable: "interfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interface_assignments_interfaces_ParentId",
                table: "interface_assignments");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "interface_properties");

            migrationBuilder.DropTable(
                name: "MinimalDTO");

            migrationBuilder.DropTable(
                name: "InterfaceDTO");

            migrationBuilder.DropTable(
                name: "interfaces");

            migrationBuilder.DropTable(
                name: "interface_assignments");
        }
    }
}
