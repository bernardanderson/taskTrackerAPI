using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace taskTracker.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CompletedOn = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d %H:%M:%S', 'now', 'localtime')"),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TaskStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
