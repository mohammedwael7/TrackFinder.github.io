using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackFinder.Migrations
{
    /// <inheritdoc />
    public partial class AchievementUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEarned",
                table: "UserBadges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "StudentCertificates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedAt",
                table: "StudentCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UnlockRequirement",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BadgeIconClass",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Badges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnlockCondition",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEarned",
                table: "UserBadges");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "StudentCertificates");

            migrationBuilder.DropColumn(
                name: "IssuedAt",
                table: "StudentCertificates");

            migrationBuilder.DropColumn(
                name: "UnlockRequirement",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "BadgeIconClass",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "UnlockCondition",
                table: "Badges");
        }
    }
}
