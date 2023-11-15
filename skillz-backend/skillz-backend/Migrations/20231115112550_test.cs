using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skillz_backend.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Varsta",
                table: "Users",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "Parola",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "NumarTelefon",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Locatie",
                table: "Users",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Bifa",
                table: "Users",
                newName: "Verified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Verified",
                table: "Users",
                newName: "Bifa");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Parola");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "NumarTelefon");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Users",
                newName: "Locatie");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "Varsta");
        }
    }
}
