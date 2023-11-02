using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityApiMe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase1102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "PointOfInterests",
                newName: "cityId");

            migrationBuilder.RenameIndex(
                name: "IX_PointOfInterests_CityId",
                table: "PointOfInterests",
                newName: "IX_PointOfInterests_cityId");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The One with the  big park.", "New York City" },
                    { 2, "The One with the cathedral that was never really finished.", "Antwerp" },
                    { 3, "The One with the  big tower.", "Paris" }
                });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[,]
                {
                    { 1, "The most visited urban park in the United States.", "Central Park", 1 },
                    { 2, "A 102-story skyscraper located in Midtown Manhattan.", "Empire State building", 1 },
                    { 3, "A Gothic style cathedral.", "Cathedral", 2 },
                    { 4, "The finest example of railway architecture in Blegium.", "Antwerp Central Station\"", 2 },
                    { 5, "A Wrought iron lattice tower on the Champ de Mars.", "Eiffel Tower", 3 },
                    { 6, "The world's largest museum.", "The Louvre", 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterests_Cities_cityId",
                table: "PointOfInterests",
                column: "cityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterests_Cities_cityId",
                table: "PointOfInterests");

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "PointOfInterests",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_PointOfInterests_cityId",
                table: "PointOfInterests",
                newName: "IX_PointOfInterests_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
