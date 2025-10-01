﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolPrj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "userRefreshToken",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "userRefreshToken");
        }
    }
}
