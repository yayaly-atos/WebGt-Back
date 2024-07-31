using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapiG2T.Migrations
{
    /// <inheritdoc />
    public partial class updatePrestataire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestataires_Utilisateur_utilisateurId",
                table: "Prestataires");

            migrationBuilder.DropIndex(
                name: "IX_Prestataires_utilisateurId",
                table: "Prestataires");

            migrationBuilder.DropColumn(
                name: "Responsable",
                table: "Prestataires");

            migrationBuilder.DropColumn(
                name: "utilisateurId",
                table: "Prestataires");

            migrationBuilder.AddColumn<int>(
                name: "PrestataireId",
                table: "Utilisateur",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_PrestataireId",
                table: "Utilisateur",
                column: "PrestataireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateur_Prestataires_PrestataireId",
                table: "Utilisateur",
                column: "PrestataireId",
                principalTable: "Prestataires",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateur_Prestataires_PrestataireId",
                table: "Utilisateur");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateur_PrestataireId",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "PrestataireId",
                table: "Utilisateur");

            migrationBuilder.AddColumn<bool>(
                name: "Responsable",
                table: "Prestataires",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "utilisateurId",
                table: "Prestataires",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prestataires_utilisateurId",
                table: "Prestataires",
                column: "utilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestataires_Utilisateur_utilisateurId",
                table: "Prestataires",
                column: "utilisateurId",
                principalTable: "Utilisateur",
                principalColumn: "Id");
        }
    }
}
