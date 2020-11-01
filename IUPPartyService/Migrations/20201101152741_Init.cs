using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IUPPartyService.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    MaxPeople = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    Host = table.Column<string>(nullable: false),
                    HostName = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    RequirePassword = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ParticipantKennitala = table.Column<string>(nullable: false),
                    ParticipantName = table.Column<string>(nullable: false),
                    EventRef = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ParticipantKennitala);
                    table.ForeignKey(
                        name: "FK_Participant_Events_EventRef",
                        column: x => x.EventRef,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_EventRef",
                table: "Participant",
                column: "EventRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
