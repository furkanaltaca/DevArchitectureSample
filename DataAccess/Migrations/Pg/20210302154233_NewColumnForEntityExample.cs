using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations.Pg
{
    public partial class NewColumnForEntityExample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "EntityExamples",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "EntityExamples");
        }
    }
}
