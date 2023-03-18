using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masters.Migrations
{
    /// <inheritdoc />
    public partial class mm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRole_AspNetRoles_RoleId",
                table: "AspNetUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRole_AspNetUsers_UserId",
                table: "AspNetUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRole",
                table: "AspNetUserRole");

            migrationBuilder.RenameTable(
                name: "AspNetUserRole",
                newName: "AspNetUserRoles");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "(N'')");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRole",
                table: "AspNetUserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRole_AspNetRoles_RoleId",
                table: "AspNetUserRole",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRole_AspNetUsers_UserId",
                table: "AspNetUserRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
