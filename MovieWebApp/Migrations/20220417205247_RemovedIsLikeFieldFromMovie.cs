using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieWebApp.Migrations
{
    public partial class RemovedIsLikeFieldFromMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "MovieRatings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "MovieRatings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
