using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental_V2.Server.Data.Migrations
{
    public partial class made_user_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Movies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
