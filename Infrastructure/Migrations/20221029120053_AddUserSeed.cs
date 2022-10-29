using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("eef47149-d7d1-4296-ae0e-3ff6421598ec"), "41DB063E-C8F8-437D-917C-72C0AC4EBB90", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsAdmin", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("794a3441-6cda-47a2-b194-7422cf5a9467"), 0, "none", "c94b51e5-52f3-4a06-a91b-f22a1588f9a4", "admin@mail.test", false, "Admin", false, "Adminov", false, null, "Adminovich", "ADMIN@MAIL.TEST", "ADMIN", "AQAAAAEAACcQAAAAECrQlU6iHvBiY61mmWiiB/4mHK0BUVhK/cAGyASJkNYKELzwy5Tb2W3tMHdXed1HAA==", "0556646400", false, "0382afaf-aeae-47ef-983d-c194ba94c64e", 1, false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("eef47149-d7d1-4296-ae0e-3ff6421598ec"), new Guid("794a3441-6cda-47a2-b194-7422cf5a9467") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("eef47149-d7d1-4296-ae0e-3ff6421598ec"), new Guid("794a3441-6cda-47a2-b194-7422cf5a9467") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eef47149-d7d1-4296-ae0e-3ff6421598ec"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("794a3441-6cda-47a2-b194-7422cf5a9467"));
        }
    }
}
