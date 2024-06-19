using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental_V2.Server.Data.Migrations
{
    public partial class added_user_movies_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_OwnerId",
                table: "Movies",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_OwnerId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Movies");
        }
    }
}
