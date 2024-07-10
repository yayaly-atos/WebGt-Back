using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapiG2T.Migrations
{
    /// <inheritdoc />
    public partial class secondUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_TypeServices_TypeServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Comptes");

            migrationBuilder.AlterColumn<int>(
                name: "TypeServiceId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TypeService",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Commentaire",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "StatutIncident",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatutFacture",
                table: "Factures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatutContact",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TypeCompte",
                table: "Comptes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "StatutCompte",
                table: "Comptes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_TypeServices_TypeServiceId",
                table: "Services",
                column: "TypeServiceId",
                principalTable: "TypeServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_TypeServices_TypeServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TypeService",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "StatutIncident",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "StatutFacture",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "StatutContact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "StatutCompte",
                table: "Comptes");

            migrationBuilder.AlterColumn<int>(
                name: "TypeServiceId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Commentaire",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TypeCompte",
                table: "Comptes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Comptes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_TypeServices_TypeServiceId",
                table: "Services",
                column: "TypeServiceId",
                principalTable: "TypeServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
