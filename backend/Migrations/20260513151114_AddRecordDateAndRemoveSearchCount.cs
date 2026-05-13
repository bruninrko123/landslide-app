using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordDateAndRemoveSearchCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HotSpots",
                table: "HotSpots");

            migrationBuilder.DropColumn(
                name: "SearchCount",
                table: "HotSpots");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "HotSpots",
                newName: "RecordDate");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "HotSpots",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotSpots",
                table: "HotSpots",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HotSpots",
                table: "HotSpots");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HotSpots");

            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "HotSpots",
                newName: "LastUpdated");

            migrationBuilder.AddColumn<int>(
                name: "SearchCount",
                table: "HotSpots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotSpots",
                table: "HotSpots",
                column: "CityName");
        }
    }
}
