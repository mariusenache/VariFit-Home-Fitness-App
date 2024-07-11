using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Muscles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupOfMuscle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuscleExercises",
                columns: table => new
                {
                    MuscleId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleExercises", x => new { x.MuscleId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Muscles_MuscleId",
                        column: x => x.MuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bodyweight" },
                    { 2, "Dumbbell" },
                    { 3, "Barbell" },
                    { 4, "Kettlebell" },
                    { 5, "Band" },
                    { 6, "Bench" }
                });

            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "GroupOfMuscle", "Name" },
                values: new object[,]
                {
                    { 1, "Arms", "Bicep" },
                    { 2, "Arms", "Tricep" },
                    { 3, "Chest", "Chest" },
                    { 4, "Back", "Shoulders" },
                    { 5, "Back", "Traps" },
                    { 6, "Back", "Back" },
                    { 7, "Legs", "Quadriceps" },
                    { 8, "Legs", "Glutes" },
                    { 9, "Legs", "Hamstrings" },
                    { 10, "Legs", "Calves" },
                    { 11, "Core", "Abs" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "EquipmentId", "Level", "Name" },
                values: new object[,]
                {
                    { 1, 2, 3, "Xbody_hammer_curl" },
                    { 2, 2, 4, "Db_trifecta" },
                    { 3, 2, 2, "Standing_curl" },
                    { 4, 2, 3, "Alternate_st_curl" },
                    { 5, 2, 2, "Lying_tricep_ext" },
                    { 6, 2, 1, "Tricep_kickback" },
                    { 7, 2, 2, "Bench_dip" },
                    { 8, 1, 1, "Close_grip_db_pushup" },
                    { 9, 1, 2, "Cobra_pushup" },
                    { 10, 1, 1, "Pushup" },
                    { 11, 2, 3, "Floor_fly" },
                    { 12, 2, 2, "Underhand_press" },
                    { 13, 1, 1, "Decline_pushup" },
                    { 14, 2, 3, "Ucv_raise" },
                    { 15, 2, 3, "Overhead_press" },
                    { 16, 2, 2, "Hip_hugger" },
                    { 17, 2, 3, "Lateral_raise" },
                    { 18, 2, 2, "Front_raise" },
                    { 19, 2, 4, "Scoop_press" },
                    { 20, 2, 3, "Reverse_fly" },
                    { 21, 2, 2, "Figure_8" },
                    { 22, 2, 3, "Press_out" },
                    { 23, 2, 3, "Renegade_row" },
                    { 24, 2, 4, "Tripod_row" },
                    { 25, 2, 3, "High_pull" },
                    { 26, 2, 4, "Man_maker" },
                    { 27, 2, 2, "W_raise" },
                    { 28, 2, 3, "Standing_row" },
                    { 29, 1, 3, "Bulgarian_split_squat" },
                    { 30, 1, 3, "Reverse_lunge" },
                    { 31, 1, 4, "Squat" },
                    { 32, 1, 2, "Calf_raise" },
                    { 33, 1, 5, "Never_ending_squat" },
                    { 34, 1, 4, "Reverse_creeping_lunge" },
                    { 35, 3, 5, "Deadlift" },
                    { 36, 1, 3, "Lunge" },
                    { 37, 1, 2, "Reverse_crunch" },
                    { 38, 1, 3, "Mountain_climber" },
                    { 39, 1, 2, "Halos" },
                    { 40, 1, 1, "Crunch" }
                });

            migrationBuilder.InsertData(
                table: "MuscleExercises",
                columns: new[] { "ExerciseId", "MuscleId", "EquipmentId" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 2, 1, null },
                    { 3, 1, null },
                    { 4, 1, null },
                    { 5, 2, null },
                    { 6, 2, null },
                    { 7, 2, null },
                    { 8, 2, null },
                    { 9, 2, null },
                    { 10, 2, null },
                    { 8, 3, null },
                    { 10, 3, null },
                    { 11, 3, null },
                    { 12, 3, null },
                    { 13, 3, null },
                    { 14, 3, null },
                    { 10, 4, null },
                    { 11, 4, null },
                    { 13, 4, null },
                    { 14, 4, null },
                    { 15, 4, null },
                    { 16, 4, null },
                    { 17, 4, null },
                    { 18, 4, null },
                    { 19, 4, null },
                    { 20, 4, null },
                    { 21, 4, null },
                    { 22, 4, null },
                    { 23, 4, null },
                    { 25, 4, null },
                    { 26, 4, null },
                    { 27, 4, null },
                    { 28, 4, null },
                    { 14, 5, null },
                    { 17, 5, null },
                    { 14, 6, null },
                    { 23, 6, null },
                    { 24, 6, null },
                    { 25, 6, null },
                    { 26, 6, null },
                    { 28, 6, null },
                    { 29, 7, null },
                    { 30, 7, null },
                    { 31, 7, null },
                    { 31, 8, null },
                    { 32, 9, null },
                    { 34, 9, null },
                    { 35, 9, null },
                    { 32, 10, null },
                    { 36, 11, null },
                    { 37, 11, null },
                    { 38, 11, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_EquipmentId",
                table: "Exercises",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_EquipmentId",
                table: "MuscleExercises",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_ExerciseId",
                table: "MuscleExercises",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Muscles");

            migrationBuilder.DropTable(
                name: "Equipments");
        }
    }
}
