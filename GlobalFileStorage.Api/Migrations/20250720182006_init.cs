using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalFileStorage.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_name = table.Column<string>(type: "text", nullable: false),
                    subdomain_prefix = table.Column<string>(type: "text", nullable: false),
                    tenant_status = table.Column<int>(type: "integer", nullable: false),
                    billing_plan = table.Column<int>(type: "integer", nullable: false),
                    storage_quota = table.Column<long>(type: "bigint", nullable: false),
                    bandwidth_quota = table.Column<long>(type: "bigint", nullable: false),
                    API_rate_limit = table.Column<int>(type: "integer", nullable: false),
                    data_residency_region = table.Column<string>(type: "text", nullable: false),
                    comliance_requirements = table.Column<int>(type: "integer", nullable: false),
                    encryption_requirements = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UsageStats",
                columns: table => new
                {
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    storage_used = table.Column<long>(type: "bigint", nullable: false),
                    bandwidth_used = table.Column<long>(type: "bigint", nullable: false),
                    API_calss_count = table.Column<int>(type: "integer", nullable: false),
                    file_operation_count = table.Column<int>(type: "integer", nullable: false),
                    active_user_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageStats", x => x.tenant_id);
                    table.ForeignKey(
                        name: "FK_UsageStats_Tenant_TenantId1",
                        column: x => x.TenantId1,
                        principalTable: "Tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    session_timeout = table.Column<TimeSpan>(type: "interval", nullable: false),
                    last_login_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    permission_json = table.Column<string>(type: "text", nullable: false),
                    MFA_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    API_key_hash = table.Column<string>(type: "text", nullable: false),
                    IP_whitelist = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileItem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    storage_path = table.Column<string>(type: "text", nullable: false),
                    MD5_hash = table.Column<string>(type: "text", nullable: false),
                    SHA256_hash = table.Column<string>(type: "text", nullable: false),
                    encryption_key_id = table.Column<Guid>(type: "uuid", nullable: false),
                    upload_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_accessed_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version_number = table.Column<int>(type: "integer", nullable: false),
                    metadata = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    tags = table.Column<List<string>>(type: "text[]", nullable: false),
                    access_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                    table.ForeignKey(
                        name: "FK_FileItem_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileItem_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileItem_TenantId",
                table: "FileItem",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FileItem_UserId",
                table: "FileItem",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageStats_TenantId1",
                table: "UsageStats",
                column: "TenantId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                table: "User",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileItem");

            migrationBuilder.DropTable(
                name: "UsageStats");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
