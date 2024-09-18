using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryFromAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "116b03f5-c765-4b7a-8e36-77b8dbc41dc5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "caea4bf7-e49a-4463-a63d-d314f5026c20");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9f211bce-7fb6-4ada-a702-2c617e6a539b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0fac9ea8-576f-4c0c-9d1b-2d765211ac39");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Image", "Name" },
                values: new object[] { null, "Health & Fitness" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[] { 3, null, "AI" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8699));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8903));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8911));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8917));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8923));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8966));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8982));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(9001));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(9012));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 395, DateTimeKind.Local).AddTicks(9018));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 3 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 18, 0, 52, 19, 409, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 4 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 18, 1, 52, 19, 409, DateTimeKind.Local).AddTicks(7991));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8bf240cc-bc18-4dd4-9c8f-9718529ae531");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "63d56bc9-5d02-45f7-9081-5740f3a2ee56");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e46b03c2-b3e2-4022-96ac-05ab9065b918");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "311fb445-12e8-4309-ba42-99b2d5a87405");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sporting");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9915));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9970));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9971));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9973));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9976));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 990, DateTimeKind.Local).AddTicks(9977));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 991, DateTimeKind.Local).AddTicks(4));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 991, DateTimeKind.Local).AddTicks(6));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 991, DateTimeKind.Local).AddTicks(7));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 991, DateTimeKind.Local).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 991, DateTimeKind.Local).AddTicks(10));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 3 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 17, 7, 35, 3, 991, DateTimeKind.Local).AddTicks(7205));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 4 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 17, 8, 35, 3, 991, DateTimeKind.Local).AddTicks(7221));
        }
    }
}
