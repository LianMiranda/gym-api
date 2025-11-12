using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdat = table.Column<DateTime>(name: "created-at", type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 492, DateTimeKind.Utc).AddTicks(9168)),
                    updatedat = table.Column<DateTime>(name: "updated-at", type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 492, DateTimeKind.Utc).AddTicks(9427))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "body_measurements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    measurement_date = table.Column<DateOnly>(type: "date", nullable: true),
                    weight = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    height = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    body_fat = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    chest = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    waist = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    hips = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    biceps = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    thighs = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    calves = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(name: "created-at", type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 485, DateTimeKind.Utc).AddTicks(3777)),
                    updatedat = table.Column<DateTime>(name: "updated-at", type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 487, DateTimeKind.Utc).AddTicks(664))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_body_measurements", x => x.id);
                    table.ForeignKey(
                        name: "FK_body_measurements_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_body_measurements_user_id",
                table: "body_measurements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "body_measurements");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
