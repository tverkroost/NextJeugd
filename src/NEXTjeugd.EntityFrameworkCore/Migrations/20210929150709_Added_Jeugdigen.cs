using Microsoft.EntityFrameworkCore.Migrations;

namespace NEXTjeugd.Migrations
{
    public partial class Added_Jeugdigen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Personen",
                newName: "Werkaantekening");

            migrationBuilder.RenameColumn(
                name: "Naam",
                table: "Personen",
                newName: "Notitie");

            migrationBuilder.AddColumn<string>(
                name: "EmailHuisarts",
                table: "Personen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GeheimDossier",
                table: "Personen",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InzageEigenDossier",
                table: "Personen",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NaamHuisarts",
                table: "Personen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SamenstellingHuishouden",
                table: "Personen",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ToestemmingInformatiedeling",
                table: "Personen",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Woonsituatie",
                table: "Personen",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailHuisarts",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "GeheimDossier",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "InzageEigenDossier",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "NaamHuisarts",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "SamenstellingHuishouden",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "ToestemmingInformatiedeling",
                table: "Personen");

            migrationBuilder.DropColumn(
                name: "Woonsituatie",
                table: "Personen");

            migrationBuilder.RenameColumn(
                name: "Werkaantekening",
                table: "Personen",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Notitie",
                table: "Personen",
                newName: "Naam");
        }
    }
}
