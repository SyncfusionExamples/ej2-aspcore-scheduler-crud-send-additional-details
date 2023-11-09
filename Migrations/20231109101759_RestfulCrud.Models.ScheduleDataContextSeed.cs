using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulCrud.Migrations
{
    /// <inheritdoc />
    public partial class RestfulCrudModelsScheduleDataContextSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ScheduleEventCollection");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ScheduleEventCollection",
                newName: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "ScheduleEventCollection",
                newName: "ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ScheduleEventCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ScheduleEventCollection",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);
        }
    }
}
