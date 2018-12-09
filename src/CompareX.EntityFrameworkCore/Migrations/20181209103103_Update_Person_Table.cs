using Microsoft.EntityFrameworkCore.Migrations;

namespace CompareX.Migrations
{
    public partial class Update_Person_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "AppPersons",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AppPersons",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "AppPersons");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AppPersons");
        }
    }
}
