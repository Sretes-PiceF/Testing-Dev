using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevHabit.Api.Migrations.Application;

/// <inheritdoc />
public partial class RenameCreatedAtToCreatedAtUtc : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "created_at",
            schema: "dev_habbit",
            table: "habits",
            newName: "created_at_utc");

        migrationBuilder.AlterColumn<string>(
            name: "target_unit",
            schema: "dev_habbit",
            table: "habits",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(long),
            oldType: "bigint",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<int>(
            name: "milestone_current",
            schema: "dev_habbit",
            table: "habits",
            type: "integer",
            nullable: true,
            oldClrType: typeof(long),
            oldType: "bigint",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "created_at_utc",
            schema: "dev_habbit",
            table: "habits",
            newName: "created_at");

        migrationBuilder.AlterColumn<long>(
            name: "target_unit",
            schema: "dev_habbit",
            table: "habits",
            type: "bigint",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<long>(
            name: "milestone_current",
            schema: "dev_habbit",
            table: "habits",
            type: "bigint",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);
    }
}
