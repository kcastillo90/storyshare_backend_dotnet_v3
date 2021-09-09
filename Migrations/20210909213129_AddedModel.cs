using Microsoft.EntityFrameworkCore.Migrations;

namespace storyshare_backend_dotnet_v3.Migrations
{
    public partial class AddedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "synopsis",
                table: "Stories",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "synopsis",
                table: "Stories");
        }
    }
}
