using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddingLatesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated-at",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "created-at",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "updated-at",
                table: "body_measurements",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "created-at",
                table: "body_measurements",
                newName: "created_at");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 492, DateTimeKind.Utc).AddTicks(9427));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 492, DateTimeKind.Utc).AddTicks(9168));

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "waist",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "thighs",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "hips",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "height",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "chest",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "calves",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "body_fat",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "biceps",
                table: "body_measurements",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "body_measurements",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 487, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "body_measurements",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 485, DateTimeKind.Utc).AddTicks(3777));

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "body_measurements",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    muscle_group = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    equipment = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    difficulty_level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    image_url = table.Column<string>(type: "nvarchar", nullable: true),
                    video_url = table.Column<string>(type: "nvarchar", nullable: true),
                    external_api_id = table.Column<string>(type: "nvarchar", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workout_plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    days_per_week = table.Column<byte>(type: "tinyint", nullable: true),
                    months = table.Column<byte>(type: "tinyint", nullable: true),
                    goal = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workout_plans", x => x.id);
                    table.ForeignKey(
                        name: "FK_workout_plans_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workout_plan_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    order_index = table.Column<byte>(type: "tinyint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routines", x => x.id);
                    table.ForeignKey(
                        name: "FK_routines_workout_plans_workout_plan_id",
                        column: x => x.workout_plan_id,
                        principalTable: "workout_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routine_exercise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    routine_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_index = table.Column<byte>(type: "tinyint", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routine_exercise", x => x.id);
                    table.ForeignKey(
                        name: "FK_routine_exercise_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_routine_exercise_routines_routine_id",
                        column: x => x.routine_id,
                        principalTable: "routines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workout_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    routine_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    duration_minutes = table.Column<TimeSpan>(type: "time", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workout_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_workout_sessions_routines_routine_id",
                        column: x => x.routine_id,
                        principalTable: "routines",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_workout_sessions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_sets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    routine_exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    set_number = table.Column<byte>(type: "tinyint", nullable: false),
                    reps = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    weight = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    rest_time_seconds = table.Column<int>(type: "int", nullable: true),
                    completed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    rpe = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_sets", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercise_sets_routine_exercise_routine_exercise_id",
                        column: x => x.routine_exercise_id,
                        principalTable: "routine_exercise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exercise_sets_routine_exercise_id",
                table: "exercise_sets",
                column: "routine_exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_exercise_sets_routine_exercise_id_set_number",
                table: "exercise_sets",
                columns: new[] { "routine_exercise_id", "set_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_routine_exercise_exercise_id",
                table: "routine_exercise",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_routine_exercise_routine_id",
                table: "routine_exercise",
                column: "routine_id");

            migrationBuilder.CreateIndex(
                name: "IX_routines_workout_plan_id",
                table: "routines",
                column: "workout_plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_workout_plans_user_id",
                table: "workout_plans",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_workout_sessions_routine_id",
                table: "workout_sessions",
                column: "routine_id");

            migrationBuilder.CreateIndex(
                name: "IX_workout_sessions_user_id",
                table: "workout_sessions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exercise_sets");

            migrationBuilder.DropTable(
                name: "workout_sessions");

            migrationBuilder.DropTable(
                name: "routine_exercise");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "routines");

            migrationBuilder.DropTable(
                name: "workout_plans");

            migrationBuilder.DropColumn(
                name: "name",
                table: "body_measurements");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "users",
                newName: "updated-at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "created-at");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "body_measurements",
                newName: "updated-at");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "body_measurements",
                newName: "created-at");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated-at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 492, DateTimeKind.Utc).AddTicks(9427),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created-at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 492, DateTimeKind.Utc).AddTicks(9168),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "waist",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "thighs",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "hips",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "height",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "chest",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "calves",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "body_fat",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "biceps",
                table: "body_measurements",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated-at",
                table: "body_measurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 487, DateTimeKind.Utc).AddTicks(664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created-at",
                table: "body_measurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 11, 12, 2, 1, 22, 485, DateTimeKind.Utc).AddTicks(3777),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
