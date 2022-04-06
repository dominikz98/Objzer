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
                name: "CTEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "enumerations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enumerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_enumerations_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Changes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_history_CTEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "CTEntity",
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
                        name: "FK_objects_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enumeration_properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnumerationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enumeration_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_enumeration_properties_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_enumeration_properties_enumerations_EnumerationId",
                        column: x => x.EnumerationId,
                        principalTable: "enumerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "object_properties",
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
                    table.PrimaryKey("PK_object_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_object_properties_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_object_properties_objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "objects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "abstraction_assignments",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abstraction_assignments", x => x.ParentId);
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
                        name: "FK_abstractions_abstraction_assignments_CTAbstractionAssignmentParentId",
                        column: x => x.CTAbstractionAssignmentParentId,
                        principalTable: "abstraction_assignments",
                        principalColumn: "ParentId");
                    table.ForeignKey(
                        name: "FK_abstractions_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abstraction_properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AbstractionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abstraction_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abstraction_properties_abstractions_AbstractionId",
                        column: x => x.AbstractionId,
                        principalTable: "abstractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_abstraction_properties_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAbstractionCTObject",
                columns: table => new
                {
                    AbstractionsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ObjectsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAbstractionCTObject", x => new { x.AbstractionsId, x.ObjectsId });
                    table.ForeignKey(
                        name: "FK_CTAbstractionCTObject_abstractions_AbstractionsId",
                        column: x => x.AbstractionsId,
                        principalTable: "abstractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAbstractionCTObject_objects_ObjectsId",
                        column: x => x.ObjectsId,
                        principalTable: "objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTInterfaceCTObject",
                columns: table => new
                {
                    InterfacesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ObjectsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTInterfaceCTObject", x => new { x.InterfacesId, x.ObjectsId });
                    table.ForeignKey(
                        name: "FK_CTInterfaceCTObject_objects_ObjectsId",
                        column: x => x.ObjectsId,
                        principalTable: "objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interface_assignments",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    CTInterfaceAssignmentParentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interfaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interfaces_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    InterfaceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interface_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interface_properties_CTEntity_Id",
                        column: x => x.Id,
                        principalTable: "CTEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_interface_properties_interfaces_InterfaceId",
                        column: x => x.InterfaceId,
                        principalTable: "interfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_abstraction_properties_AbstractionId",
                table: "abstraction_properties",
                column: "AbstractionId");

            migrationBuilder.CreateIndex(
                name: "IX_abstractions_CTAbstractionAssignmentParentId",
                table: "abstractions",
                column: "CTAbstractionAssignmentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CTAbstractionCTObject_ObjectsId",
                table: "CTAbstractionCTObject",
                column: "ObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_CTInterfaceCTObject_ObjectsId",
                table: "CTInterfaceCTObject",
                column: "ObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_enumeration_properties_EnumerationId",
                table: "enumeration_properties",
                column: "EnumerationId");

            migrationBuilder.CreateIndex(
                name: "IX_history_EntityId",
                table: "history",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_interface_properties_InterfaceId",
                table: "interface_properties",
                column: "InterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_interfaces_CTInterfaceAssignmentParentId",
                table: "interfaces",
                column: "CTInterfaceAssignmentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_object_properties_ObjectId",
                table: "object_properties",
                column: "ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_abstraction_assignments_abstractions_ParentId",
                table: "abstraction_assignments",
                column: "ParentId",
                principalTable: "abstractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CTInterfaceCTObject_interfaces_InterfacesId",
                table: "CTInterfaceCTObject",
                column: "InterfacesId",
                principalTable: "interfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_abstraction_assignments_abstractions_ParentId",
                table: "abstraction_assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_interfaces_CTEntity_Id",
                table: "interfaces");

            migrationBuilder.DropForeignKey(
                name: "FK_interface_assignments_interfaces_ParentId",
                table: "interface_assignments");

            migrationBuilder.DropTable(
                name: "abstraction_properties");

            migrationBuilder.DropTable(
                name: "CTAbstractionCTObject");

            migrationBuilder.DropTable(
                name: "CTInterfaceCTObject");

            migrationBuilder.DropTable(
                name: "enumeration_properties");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "interface_properties");

            migrationBuilder.DropTable(
                name: "object_properties");

            migrationBuilder.DropTable(
                name: "enumerations");

            migrationBuilder.DropTable(
                name: "objects");

            migrationBuilder.DropTable(
                name: "abstractions");

            migrationBuilder.DropTable(
                name: "abstraction_assignments");

            migrationBuilder.DropTable(
                name: "CTEntity");

            migrationBuilder.DropTable(
                name: "interfaces");

            migrationBuilder.DropTable(
                name: "interface_assignments");
        }
    }
}
