using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeSpokedBikes.Data.Migrations
{
    public partial class EntitiesSchemaFixDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SellDateTime",
                table: "Sales",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellDateTime",
                table: "Sales");
        }
    }
}
