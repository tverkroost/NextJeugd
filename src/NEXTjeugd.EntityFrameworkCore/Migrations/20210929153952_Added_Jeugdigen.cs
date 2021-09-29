using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NEXTjeugd.Migrations
{
    public partial class Added_Jeugdigen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPersonen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Roepnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voorletters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tussenvoegsel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BSN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geslacht = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Geboorteland = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Woonsituatie = table.Column<int>(type: "int", nullable: true),
                    SamenstellingHuishouden = table.Column<int>(type: "int", nullable: true),
                    ToestemmingInformatiedeling = table.Column<bool>(type: "bit", nullable: true),
                    Notitie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Werkaantekening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InzageEigenDossier = table.Column<bool>(type: "bit", nullable: true),
                    GeheimDossier = table.Column<bool>(type: "bit", nullable: true),
                    NaamHuisarts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailHuisarts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPersonen", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPersonen");
        }
    }
}
