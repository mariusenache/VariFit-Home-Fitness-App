using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addfavoriteworkoutsmanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteWorkouts",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteWorkouts", x => new { x.ApplicationUserId, x.WorkoutId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteWorkouts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteWorkouts_WorkoutId",
                table: "UserFavoriteWorkouts",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteWorkouts");
        }
    }
}
