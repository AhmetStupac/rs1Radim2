﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_25.API.Migrations
{
    /// <inheritdoc />
    public partial class addedYOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.CreateTable(
                name: "YearOfStudies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkademskaGodinaId = table.Column<int>(type: "int", nullable: false),
                    GodinaStudija = table.Column<int>(type: "int", nullable: false),
                    Obnova = table.Column<bool>(type: "bit", nullable: false),
                    DatumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SnimioId = table.Column<int>(type: "int", nullable: false),
                    CijenaSkolarine = table.Column<float>(type: "real", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearOfStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearOfStudies_AcademicYears_AkademskaGodinaId",
                        column: x => x.AkademskaGodinaId,
                        principalTable: "AcademicYears",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_YearOfStudies_MyAppUsers_SnimioId",
                        column: x => x.SnimioId,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_YearOfStudies_MyAppUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_YearOfStudies_AkademskaGodinaId",
                table: "YearOfStudies",
                column: "AkademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_YearOfStudies_SnimioId",
                table: "YearOfStudies",
                column: "SnimioId");

            migrationBuilder.CreateIndex(
                name: "IX_YearOfStudies_StudentId",
                table: "YearOfStudies",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YearOfStudies");

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkademskaGodinaId = table.Column<int>(type: "int", nullable: false),
                    SnimioId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CijenaSkolarine = table.Column<float>(type: "real", nullable: false),
                    DatumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GodinaStudija = table.Column<int>(type: "int", nullable: false),
                    Obnova = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_AcademicYears_AkademskaGodinaId",
                        column: x => x.AkademskaGodinaId,
                        principalTable: "AcademicYears",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Semesters_MyAppUsers_SnimioId",
                        column: x => x.SnimioId,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Semesters_MyAppUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_AkademskaGodinaId",
                table: "Semesters",
                column: "AkademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SnimioId",
                table: "Semesters",
                column: "SnimioId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudentId",
                table: "Semesters",
                column: "StudentId");
        }
    }
}
