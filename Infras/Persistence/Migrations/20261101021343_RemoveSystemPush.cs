using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Infras.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSystemPush : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemPushSubscription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemPushSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Auth = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    ExpirationTime = table.Column<long>(type: "bigint", nullable: true),
                    P256dh = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPushSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemPushSubscription_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemPushSubscription_Endpoint",
                table: "SystemPushSubscription",
                column: "Endpoint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemPushSubscription_ProfileId",
                table: "SystemPushSubscription",
                column: "ProfileId");
        }
    }
}
