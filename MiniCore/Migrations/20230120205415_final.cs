using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniCore.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Cliente_ClienteId",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Contrato",
                newName: "ClienteID");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ClienteId",
                table: "Contrato",
                newName: "IX_Contrato_ClienteID");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteID",
                table: "Contrato",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Cliente_ClienteID",
                table: "Contrato",
                column: "ClienteID",
                principalTable: "Cliente",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Cliente_ClienteID",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "Contrato",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ClienteID",
                table: "Contrato",
                newName: "IX_Contrato_ClienteId");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Contrato",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Cliente_ClienteId",
                table: "Contrato",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "ClienteId");
        }
    }
}
