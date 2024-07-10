using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapiG2T.Migrations
{
    /// <inheritdoc />
    public partial class firstUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_EntiteEnCharge_EntiteEnChargeId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateur_EntiteEnCharge_EntiteEnChargeId",
                table: "Utilisateur");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateur_Profils_ProfilId",
                table: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Profils");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateur_ProfilId",
                table: "Utilisateur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntiteEnCharge",
                table: "EntiteEnCharge");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "ProfilId",
                table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "telephone",
                table: "Utilisateur");

            migrationBuilder.RenameTable(
                name: "EntiteEnCharge",
                newName: "EntiteEnCharges");

            migrationBuilder.AlterColumn<int>(
                name: "Statut",
                table: "Incidents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Statut",
                table: "Factures",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Statut",
                table: "Contacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TypeCompte",
                table: "Comptes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Statut",
                table: "Comptes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Responsable",
                table: "EntiteEnCharges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntiteEnCharges",
                table: "EntiteEnCharges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ContactId",
                table: "Incidents",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Contacts_ContactId",
                table: "Incidents",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_EntiteEnCharges_EntiteEnChargeId",
                table: "Incidents",
                column: "EntiteEnChargeId",
                principalTable: "EntiteEnCharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateur_EntiteEnCharges_EntiteEnChargeId",
                table: "Utilisateur",
                column: "EntiteEnChargeId",
                principalTable: "EntiteEnCharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Contacts_ContactId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_EntiteEnCharges_EntiteEnChargeId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateur_EntiteEnCharges_EntiteEnChargeId",
                table: "Utilisateur");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_ContactId",
                table: "Incidents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntiteEnCharges",
                table: "EntiteEnCharges");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Responsable",
                table: "EntiteEnCharges");

            migrationBuilder.RenameTable(
                name: "EntiteEnCharges",
                newName: "EntiteEnCharge");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProfilId",
                table: "Utilisateur",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "telephone",
                table: "Utilisateur",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Factures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TypeCompte",
                table: "Comptes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Comptes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntiteEnCharge",
                table: "EntiteEnCharge",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Profils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profils", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_ProfilId",
                table: "Utilisateur",
                column: "ProfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_EntiteEnCharge_EntiteEnChargeId",
                table: "Incidents",
                column: "EntiteEnChargeId",
                principalTable: "EntiteEnCharge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateur_EntiteEnCharge_EntiteEnChargeId",
                table: "Utilisateur",
                column: "EntiteEnChargeId",
                principalTable: "EntiteEnCharge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateur_Profils_ProfilId",
                table: "Utilisateur",
                column: "ProfilId",
                principalTable: "Profils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
