using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAbbsMuscleForHalosAndCrunch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MuscleExercises",
                columns: new[] { "ExerciseId", "MuscleId", "EquipmentId" },
                values: new object[,]
                {
                    { 39, 11, null },
                    { 40, 11, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MuscleExercises",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 39, 11 });

            migrationBuilder.DeleteData(
                table: "MuscleExercises",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 40, 11 });
        }
    }
}
