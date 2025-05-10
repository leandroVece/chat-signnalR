using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id_user = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    contra = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id_user);
                });

            migrationBuilder.CreateTable(
                name: "Conversaciones",
                columns: table => new
                {
                    id_Conversacion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id_Emisor = table.Column<int>(type: "INTEGER", nullable: false),
                    id_remitente = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversaciones", x => x.id_Conversacion);
                    table.ForeignKey(
                        name: "FK_Conversaciones_Users_Id_Emisor",
                        column: x => x.Id_Emisor,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversaciones_Users_id_remitente",
                        column: x => x.id_remitente,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    id_mensaje = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id_Conversacion = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_user = table.Column<int>(type: "INTEGER", nullable: false),
                    contenido = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.id_mensaje);
                    table.ForeignKey(
                        name: "FK_Mensajes_Conversaciones_Id_Conversacion",
                        column: x => x.Id_Conversacion,
                        principalTable: "Conversaciones",
                        principalColumn: "id_Conversacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensajes_Users_Id_user",
                        column: x => x.Id_user,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_Id_Emisor",
                table: "Conversaciones",
                column: "Id_Emisor");

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_id_remitente",
                table: "Conversaciones",
                column: "id_remitente");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Id_Conversacion",
                table: "Mensajes",
                column: "Id_Conversacion");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Id_user",
                table: "Mensajes",
                column: "Id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Conversaciones");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
