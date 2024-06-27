using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental_V2.Server.Data.Migrations
{
    public partial class added_state_enum_to_movie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Movies",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Movies",
                newName: "Available");
        }
    }
}
