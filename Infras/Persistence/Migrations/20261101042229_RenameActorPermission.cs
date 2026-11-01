using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Infras.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameActorPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Preserve each role's existing grants; remove old duplicates before renaming.
            migrationBuilder.Sql(
                """
                DELETE FROM "RolePermission" AS permission
                USING (VALUES
                    ('ACTOR', 'AUTH_ACTOR')
                ) AS mapping(old_code, new_code)
                WHERE permission."Code" = mapping.old_code
                  AND EXISTS (
                    SELECT 1 FROM "RolePermission" AS existing
                    WHERE existing."RoleId" = permission."RoleId"
                      AND existing."Code" = mapping.new_code
                  );

                UPDATE "RolePermission" AS permission
                SET "Code" = mapping.new_code
                FROM (VALUES
                    ('ACTOR', 'AUTH_ACTOR')
                ) AS mapping(old_code, new_code)
                WHERE permission."Code" = mapping.old_code;

                UPDATE "Setting"
                SET "Value" = replace("Value", '"ACTOR"', '"AUTH_ACTOR"')
                WHERE "Name" = 'DEFAULT_PERMISSION';
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Preserve each role's existing grants; remove old duplicates before renaming.
            migrationBuilder.Sql(
                """
                DELETE FROM "RolePermission" AS permission
                USING (VALUES
                    ('AUTH_ACTOR', 'ACTOR')
                ) AS mapping(old_code, new_code)
                WHERE permission."Code" = mapping.old_code
                  AND EXISTS (
                    SELECT 1 FROM "RolePermission" AS existing
                    WHERE existing."RoleId" = permission."RoleId"
                      AND existing."Code" = mapping.new_code
                  );

                UPDATE "RolePermission" AS permission
                SET "Code" = mapping.new_code
                FROM (VALUES
                    ('AUTH_ACTOR', 'ACTOR')
                ) AS mapping(old_code, new_code)
                WHERE permission."Code" = mapping.old_code;

                UPDATE "Setting"
                SET "Value" = replace("Value", '"AUTH_ACTOR"', '"ACTOR"')
                WHERE "Name" = 'DEFAULT_PERMISSION';
                """
            );
        }
    }
}
