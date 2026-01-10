using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SiteSurveyApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateSiteLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "SiteLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cluster = table.Column<string>(type: "text", nullable: false),
                    Site_Name = table.Column<string>(type: "text", nullable: false),
                    Components = table.Column<string>(type: "text", nullable: false),
                    Subcomponents = table.Column<string>(type: "text", nullable: false),
                    Project = table.Column<string>(type: "text", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false),
                    Geom = table.Column<Point>(type: "geometry", nullable: false),
                    Photo_Path = table.Column<string>(type: "text", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteLocations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteLocations");
        }
    }
}
