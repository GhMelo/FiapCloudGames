using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migracaocorrecaopromocoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Promocao",
                table: "Promocao");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Promocao",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Promocao",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promocao",
                table: "Promocao",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Promocao_JogoId",
                table: "Promocao",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Promocao",
                table: "Promocao");

            migrationBuilder.DropIndex(
                name: "IX_Promocao_JogoId",
                table: "Promocao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Promocao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Promocao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promocao",
                table: "Promocao",
                column: "JogoId");
        }
    }
}
