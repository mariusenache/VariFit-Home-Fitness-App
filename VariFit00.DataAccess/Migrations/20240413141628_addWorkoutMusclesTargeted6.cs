using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addWorkoutMusclesTargeted6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MusclesTargeted",
                table: "Workouts",
                newName: "MusclesTargetedIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MusclesTargetedIds",
                table: "Workouts",
                newName: "MusclesTargeted");
        }
    }
}
