﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VariFit00.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addmigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MusclesTargetedIds",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusclesTargetedIds",
                table: "Workouts");
        }
    }
}
