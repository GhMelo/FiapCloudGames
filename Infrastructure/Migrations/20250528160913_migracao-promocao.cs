using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migracaopromocao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promocao",
                columns: table => new
                {
                    JogoId = table.Column<int>(type: "INT", nullable: false),
                    NomePromocao = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Porcentagem = table.Column<int>(type: "INT", nullable: false),
                    PromocaoAtiva = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocao", x => x.JogoId);
                    table.ForeignKey(
                        name: "FK_Promocao_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promocao");
        }
    }
}
