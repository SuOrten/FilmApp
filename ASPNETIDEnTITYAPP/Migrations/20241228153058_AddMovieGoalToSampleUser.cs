using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETIDEnTITYAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieGoalToSampleUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieGoal",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieGoal",
                table: "AspNetUsers");
        }
    }
}
