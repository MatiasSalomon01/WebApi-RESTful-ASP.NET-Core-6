using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class AdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"START TRANSACTION;

INSERT INTO ""AspNetRoles"" (""Id"", ""ConcurrencyStamp"", ""Name"", ""NormalizedName"")
VALUES ('ddc91b08-ff2a-4778-8a45-9c2640a9914b', 'b91de32d-6f1c-4da9-b302-9f6aa806d8df', 'Admin', 'Admin');

INSERT INTO ""AspNetUsers"" (""Id"", ""AccessFailedCount"", ""ConcurrencyStamp"", ""Email"", ""EmailConfirmed"", ""LockoutEnabled"", ""LockoutEnd"", ""NormalizedEmail"", ""NormalizedUserName"", ""PasswordHash"", ""PhoneNumber"", ""PhoneNumberConfirmed"", ""SecurityStamp"", ""TwoFactorEnabled"", ""UserName"")
VALUES ('bbf40b1f-c71c-4e88-84f9-afe705830364', 0, '3276b731-12e7-4cf9-8dc6-48081e3ad796', 'matias@gmail.com', FALSE, FALSE, NULL, 'matias@gmail.com', 'matias@gmail.com', 'AQAAAAEAACcQAAAAEPND4Twz/f8aEnYXMwHGuZ+FRwDMKLdQzQQnZwKdifp0RjYXVfn3abTXqTt5GTRBiQ==', NULL, FALSE, '6c16c524-f656-4e9a-9b21-732ed7e15b50', FALSE, 'matias@gmail.com');

INSERT INTO ""AspNetUserClaims"" (""Id"", ""ClaimType"", ""ClaimValue"", ""UserId"")
VALUES (1, 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Admin', 'bbf40b1f-c71c-4e88-84f9-afe705830364');

SELECT setval(
    pg_get_serial_sequence('""AspNetUserClaims""', 'Id'),
    GREATEST(
        (SELECT MAX(""Id"") FROM ""AspNetUserClaims"") + 1,
        nextval(pg_get_serial_sequence('""AspNetUserClaims""', 'Id'))),
    false);

COMMIT;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddc91b08-ff2a-4778-8a45-9c2640a9914b");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbf40b1f-c71c-4e88-84f9-afe705830364");
        }
    }
}
