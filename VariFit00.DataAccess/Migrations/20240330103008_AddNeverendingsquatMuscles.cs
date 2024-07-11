using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNeverendingsquatMuscles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MuscleExercises",
                columns: new[] { "ExerciseId", "MuscleId", "EquipmentId" },
                values: new object[] { 33, 7, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MuscleExercises",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 33, 7 });
        }
    }
}
