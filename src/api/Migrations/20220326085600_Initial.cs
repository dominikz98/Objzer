using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contracts_entities_Id",
                        column: x => x.Id,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    New = table.Column<string>(type: "TEXT", nullable: true),
                    Old = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_history_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_objects_entities_Id",
                        column: x => x.Id,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enumerations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Values = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enumerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_enumerations_contracts_Id",
                        column: x => x.Id,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTContractCTObject",
                columns: table => new
                {
                    ContractsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ObjectsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTContractCTObject", x => new { x.ContractsId, x.ObjectsId });
                    table.ForeignKey(
                        name: "FK_CTContractCTObject_contracts_ContractsId",
                        column: x => x.ContractsId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTContractCTObject_objects_ObjectsId",
                        column: x => x.ObjectsId,
                        principalTable: "objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Key = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Column = table.Column<string>(type: "TEXT", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxLength = table.Column<int>(type: "INTEGER", nullable: true),
                    StringLength = table.Column<int>(type: "INTEGER", nullable: true),
                    ObjectId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_properties_entities_Id",
                        column: x => x.Id,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_properties_objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "objects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "abstractions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CTAbstractionAssignmentParentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abstractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abstractions_contracts_Id",
                        column: x => x.Id,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abstractions_assignments",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abstractions_assignments", x => x.ParentId);
                    table.ForeignKey(
                        name: "FK_abstractions_assignments_abstractions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "abstractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interfaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CTInterfaceAssignmentParentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interfaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interfaces_contracts_Id",
                        column: x => x.Id,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interfaces_assignments",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interfaces_assignments", x => x.ParentId);
                    table.ForeignKey(
                        name: "FK_interfaces_assignments_interfaces_ParentId",
                        column: x => x.ParentId,
                        principalTable: "interfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_abstractions_CTAbstractionAssignmentParentId",
                table: "abstractions",
                column: "CTAbstractionAssignmentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CTContractCTObject_ObjectsId",
                table: "CTContractCTObject",
                column: "ObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_history_EntityId",
                table: "history",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_interfaces_CTInterfaceAssignmentParentId",
                table: "interfaces",
                column: "CTInterfaceAssignmentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_properties_ObjectId",
                table: "properties",
                column: "ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_abstractions_abstractions_assignments_CTAbstractionAssignmentParentId",
                table: "abstractions",
                column: "CTAbstractionAssignmentParentId",
                principalTable: "abstractions_assignments",
                principalColumn: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_interfaces_interfaces_assignments_CTInterfaceAssignmentParentId",
                table: "interfaces",
                column: "CTInterfaceAssignmentParentId",
                principalTable: "interfaces_assignments",
                principalColumn: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_abstractions_abstractions_assignments_CTAbstractionAssignmentParentId",
                table: "abstractions");

            migrationBuilder.DropForeignKey(
                name: "FK_interfaces_contracts_Id",
                table: "interfaces");

            migrationBuilder.DropForeignKey(
                name: "FK_interfaces_interfaces_assignments_CTInterfaceAssignmentParentId",
                table: "interfaces");

            migrationBuilder.DropTable(
                name: "CTContractCTObject");

            migrationBuilder.DropTable(
                name: "enumerations");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "properties");

            migrationBuilder.DropTable(
                name: "objects");

            migrationBuilder.DropTable(
                name: "abstractions_assignments");

            migrationBuilder.DropTable(
                name: "abstractions");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "entities");

            migrationBuilder.DropTable(
                name: "interfaces_assignments");

            migrationBuilder.DropTable(
                name: "interfaces");
        }
    }
}
