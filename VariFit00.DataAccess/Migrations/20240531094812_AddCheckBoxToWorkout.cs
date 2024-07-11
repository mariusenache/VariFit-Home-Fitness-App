using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckBoxToWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Workouts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Workouts");
        }
    }
}
