using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class PendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationInDays",
                table: "Plans",
                newName: "DurationDays");

            migrationBuilder.AddColumn<int>(
                name: "Address_BuildingNumber",
                table: "Trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Address_BuildingNumber",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_BuildingNumber",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Address_BuildingNumber",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "DurationDays",
                table: "Plans",
                newName: "DurationInDays");
        }
    }
}
