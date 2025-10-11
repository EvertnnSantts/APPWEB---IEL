using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEL.Migrations
{
    /// <inheritdoc />
    public partial class AlterarParaDateConclusao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBirth",
                table: "Alunos",
                newName: "DateConclusao");

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateConclusao",
                value: new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateConclusao",
                value: new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateConclusao",
                table: "Alunos",
                newName: "DateBirth");

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateBirth",
                value: new DateTime(2003, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateBirth",
                value: new DateTime(2004, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
