using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapiG2T.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canaux", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomCompte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeCompte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOuverture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatutCompte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solde = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntitesSupports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomEntite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperviseurId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitesSupports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motifs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motifs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prestataires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPrestateur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestataires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latence = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RevoquerTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRevoquer = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoquer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevoquerTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeService = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatutContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SousMotifs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotifId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousMotifs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SousMotifs_Motifs_MotifId",
                        column: x => x.MotifId,
                        principalTable: "Motifs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disponiblite = table.Column<bool>(type: "bit", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: true),
                    EntiteSupportId = table.Column<int>(type: "int", nullable: true),
                    PrestataireId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateur_EntitesSupports_EntiteSupportId",
                        column: x => x.EntiteSupportId,
                        principalTable: "EntitesSupports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Utilisateur_Prestataires_PrestataireId",
                        column: x => x.PrestataireId,
                        principalTable: "Prestataires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    CompteId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateFacturation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatePaiement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatutFacture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => new { x.CompteId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_Factures_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factures_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanalId = table.Column<int>(type: "int", nullable: false),
                    sousMotifId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatutIncident = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAffectation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEscalade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEcheance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRelance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateResolution = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Escalade = table.Column<bool>(type: "bit", nullable: false),
                    AgentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SuperviseurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeleconseillerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    NiveauDurgenceId = table.Column<int>(type: "int", nullable: false),
                    EntiteSupportId = table.Column<int>(type: "int", nullable: false),
                    CommentaireEscalade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentaireAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentaireCloture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentaireTeleconseiller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotifId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_Canaux_CanalId",
                        column: x => x.CanalId,
                        principalTable: "Canaux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_EntitesSupports_EntiteSupportId",
                        column: x => x.EntiteSupportId,
                        principalTable: "EntitesSupports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Motifs_MotifId",
                        column: x => x.MotifId,
                        principalTable: "Motifs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_Priorite_NiveauDurgenceId",
                        column: x => x.NiveauDurgenceId,
                        principalTable: "Priorite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_SousMotifs_sousMotifId",
                        column: x => x.sousMotifId,
                        principalTable: "SousMotifs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Utilisateur_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_Utilisateur_SuperviseurId",
                        column: x => x.SuperviseurId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_Utilisateur_TeleconseillerId",
                        column: x => x.TeleconseillerId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Utilisateur_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilisateur",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompteId",
                table: "Contacts",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ServiceId",
                table: "Factures",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_AgentId",
                table: "Incidents",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CanalId",
                table: "Incidents",
                column: "CanalId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ContactId",
                table: "Incidents",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_EntiteSupportId",
                table: "Incidents",
                column: "EntiteSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_MotifId",
                table: "Incidents",
                column: "MotifId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_NiveauDurgenceId",
                table: "Incidents",
                column: "NiveauDurgenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ServiceId",
                table: "Incidents",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_sousMotifId",
                table: "Incidents",
                column: "sousMotifId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_SuperviseurId",
                table: "Incidents",
                column: "SuperviseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_TeleconseillerId",
                table: "Incidents",
                column: "TeleconseillerId");

            migrationBuilder.CreateIndex(
                name: "IX_SousMotifs_MotifId",
                table: "SousMotifs",
                column: "MotifId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_EntiteSupportId",
                table: "Utilisateur",
                column: "EntiteSupportId",
                unique: true,
                filter: "[EntiteSupportId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_PrestataireId",
                table: "Utilisateur",
                column: "PrestataireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "RevoquerTokens");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Canaux");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Priorite");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SousMotifs");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "Motifs");

            migrationBuilder.DropTable(
                name: "EntitesSupports");

            migrationBuilder.DropTable(
                name: "Prestataires");
        }
    }
}
