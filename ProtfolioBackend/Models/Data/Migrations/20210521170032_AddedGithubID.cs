using Microsoft.EntityFrameworkCore.Migrations;

namespace ProtfolioBackend.Models.Data.Migrations
{
    public partial class AddedGithubID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Encoding",
                table: "Repos");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Repos",
                newName: "GithubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GithubId",
                table: "Repos",
                newName: "Size");

            migrationBuilder.AddColumn<string>(
                name: "Encoding",
                table: "Repos",
                type: "TEXT",
                nullable: true);
        }
    }
}
