using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lanre.Data.Context.Migrations.Schedule
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2018, 8, 15, 13, 57, 9, 477, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Description", "EntryDate" },
                values: new object[,]
                {
                    { new Guid("a09c1e14-d823-4281-926d-345299696beb"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("7fa0d8be-83b2-45fa-b3ce-e40b6195beaa"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("7dc436a5-29b9-475d-a394-f97a691f74f3"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("e4fb1be6-4ba7-40c7-beef-e2b8c32751b6"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("4da90981-6a19-48a1-a6d0-c1f413dc748d"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("1849dfa8-9fac-4b0c-a63f-e3198935dc7b"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("dc8fabd1-09b6-427f-8454-2066a70d2828"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("c66f9fae-ea7c-43cf-b5bf-54f9164489f8"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) },
                    { new Guid("f5950a6f-a698-4393-a9a5-5441cd03b1f7"), null, new DateTime(2018, 8, 15, 13, 57, 9, 479, DateTimeKind.Local) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
