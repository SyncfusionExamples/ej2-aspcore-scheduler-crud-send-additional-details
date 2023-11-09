using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulCrud.Migrations
{
    /// <inheritdoc />
    public partial class RestfulCrudModelsScheduleDataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleEventCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTimezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTimezone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAllDay = table.Column<bool>(type: "bit", nullable: false),
                    RecurrenceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecurrenceRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecurrenceException = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEventCollection", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ScheduleEventCollection",
                columns: new[] { "Id", "CategoryId", "Description", "EndTime", "EndTimezone", "IsAllDay", "Location", "ProjectId", "RecurrenceException", "RecurrenceID", "RecurrenceRule", "StartTime", "StartTimezone", "Subject" },
                values: new object[] { 1, 1, "Description", new DateTime(2023, 7, 4, 10, 30, 0, 0, DateTimeKind.Unspecified), null, false, "Tamilnadu", 1, null, null, null, new DateTime(2023, 7, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), null, "Meeting" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleEventCollection");
        }
    }
}
