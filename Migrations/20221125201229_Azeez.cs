using Microsoft.EntityFrameworkCore.Migrations;

namespace UNIFY.Migrations
{
    public partial class Azeez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ISReported",
                table: "Posts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISReported",
                table: "Posts");
        }
    }
}
