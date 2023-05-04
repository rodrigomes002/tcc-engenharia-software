using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthJobs.Infra.Migrations
{
    public partial class AddColunaLocal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Vagas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Local",
                table: "Vagas");
        }
    }
}
