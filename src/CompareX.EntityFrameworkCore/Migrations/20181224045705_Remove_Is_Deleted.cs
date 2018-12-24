using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompareX.Migrations
{
    public partial class Remove_Is_Deleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "CourseRegistrations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "CourseRegistrations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "CourseRegistrations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseRegistrations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "CourseRegistrations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "CourseRegistrations",
                nullable: true);
        }
    }
}
