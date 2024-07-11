using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addWorkoutMusclesTargeted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MusclesTargeted",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusclesTargeted",
                table: "Workouts");
        }
    }
}
