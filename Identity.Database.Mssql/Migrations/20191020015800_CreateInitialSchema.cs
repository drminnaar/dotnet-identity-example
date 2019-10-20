using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Database.Mssql.Migrations
{
    public partial class CreateInitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity_data");

            migrationBuilder.CreateTable(
                name: "app_role",
                schema: "identity_data",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_approle_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_user",
                schema: "identity_data",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_username = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_appuser_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_role_claim",
                schema: "identity_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<Guid>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_approleclaim_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_identitydata_approleclaim_roleid",
                        column: x => x.role_id,
                        principalSchema: "identity_data",
                        principalTable: "app_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_claim",
                schema: "identity_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<Guid>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_appuserclaim_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_identitydata_appuserclaim_userid",
                        column: x => x.user_id,
                        principalSchema: "identity_data",
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_login",
                schema: "identity_data",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_appuserlogin_loginproviderkey", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_identitydata_appuserlogin_userid",
                        column: x => x.user_id,
                        principalSchema: "identity_data",
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_role",
                schema: "identity_data",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_appuserrole_useridroleid", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_identitydata_approle_roleid",
                        column: x => x.role_id,
                        principalSchema: "identity_data",
                        principalTable: "app_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_identitydata_appuserrole_userid",
                        column: x => x.user_id,
                        principalSchema: "identity_data",
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_token",
                schema: "identity_data",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identitydata_appusertoken_loginproviderkey", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_identitydata_appusertoken_userid",
                        column: x => x.user_id,
                        principalSchema: "identity_data",
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "uix_identitydata_approle_normalizedname",
                schema: "identity_data",
                table: "app_role",
                column: "normalized_name",
                unique: true,
                filter: "[normalized_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_approleclaim_roleid",
                schema: "identity_data",
                table: "app_role_claim",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_appuser_normalizedemail",
                schema: "identity_data",
                table: "app_user",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "uix_identitydata_appuser_normalizedusername",
                schema: "identity_data",
                table: "app_user",
                column: "normalized_username",
                unique: true,
                filter: "[normalized_username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_appuserclaim_userid",
                schema: "identity_data",
                table: "app_user_claim",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_appuserlogin_userid",
                schema: "identity_data",
                table: "app_user_login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_appuserrole_roleid",
                schema: "identity_data",
                table: "app_user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_appuserrole_userid",
                schema: "identity_data",
                table: "app_user_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_identitydata_appusertoken_userid",
                schema: "identity_data",
                table: "app_user_token",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_role_claim",
                schema: "identity_data");

            migrationBuilder.DropTable(
                name: "app_user_claim",
                schema: "identity_data");

            migrationBuilder.DropTable(
                name: "app_user_login",
                schema: "identity_data");

            migrationBuilder.DropTable(
                name: "app_user_role",
                schema: "identity_data");

            migrationBuilder.DropTable(
                name: "app_user_token",
                schema: "identity_data");

            migrationBuilder.DropTable(
                name: "app_role",
                schema: "identity_data");

            migrationBuilder.DropTable(
                name: "app_user",
                schema: "identity_data");
        }
    }
}
