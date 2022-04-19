using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieWebApp.Migrations
{
    public partial class RemovedIsLikeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "ActorRatings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "ActorRatings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
