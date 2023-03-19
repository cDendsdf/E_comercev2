using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomerce.Migrations
{
    /// <inheritdoc />
    public partial class Initialass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Detalle_ProductoId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_ProductoId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_ProductoId",
                table: "Detalle",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_Producto_ProductoId",
                table: "Detalle",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_Producto_ProductoId",
                table: "Detalle");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_ProductoId",
                table: "Detalle");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Producto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_ProductoId",
                table: "Producto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Detalle_ProductoId",
                table: "Producto",
                column: "ProductoId",
                principalTable: "Detalle",
                principalColumn: "Id");
        }
    }
}
