using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migracaoremocaologsestruturados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogRequest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrelationId = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "VARCHAR(1000)", nullable: false),
                    ExecutionTimeMs = table.Column<double>(type: "FLOAT", nullable: false),
                    Method = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Path = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    StatusCode = table.Column<int>(type: "INT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogRequest", x => x.Id);
                });
        }
    }
}
