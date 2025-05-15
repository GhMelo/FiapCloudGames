using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class segundamigracaoajustepropriedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioJogoAdquirido_Usuario_UsuarioCadastroId",
                table: "UsuarioJogoAdquirido");

            migrationBuilder.RenameColumn(
                name: "UsuarioCadastroId",
                table: "UsuarioJogoAdquirido",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioJogoAdquirido_UsuarioCadastroId",
                table: "UsuarioJogoAdquirido",
                newName: "IX_UsuarioJogoAdquirido_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioJogoAdquirido_Usuario_UsuarioId",
                table: "UsuarioJogoAdquirido",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioJogoAdquirido_Usuario_UsuarioId",
                table: "UsuarioJogoAdquirido");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "UsuarioJogoAdquirido",
                newName: "UsuarioCadastroId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioJogoAdquirido_UsuarioId",
                table: "UsuarioJogoAdquirido",
                newName: "IX_UsuarioJogoAdquirido_UsuarioCadastroId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioJogoAdquirido_Usuario_UsuarioCadastroId",
                table: "UsuarioJogoAdquirido",
                column: "UsuarioCadastroId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
