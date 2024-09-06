using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Learning.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CourseRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionNumber",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "GitHub", "ProfilePicture" },
                values: new object[] { "5a09e401-ff72-43a2-802d-ae3bc67fcf0c", null, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTAM-SBzUfYOMhwc0o76MpvR7N4Yi43lcYt5g&s" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "GitHub", "ProfilePicture" },
                values: new object[] { "180bb195-b6e8-4c64-b1cd-93b90fa9b086", null, "https://pbs.twimg.com/profile_images/1745781333400399872/MN7Wm4Ya_400x400.jpg" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "GitHub" },
                values: new object[] { "cf085d69-d743-470a-8e92-99cbee542d5a", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "GitHub" },
                values: new object[] { "a2f529c9-98bb-47aa-bc9a-8691128d4635", null });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQPvJBvVedFjpONzC1ZOR-YSWauBp9ZKK6ydA&s", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7677), new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2o9_OmdunGnBKDIiSGo3uLYvA8vySqQ-M9fyVT_nys9HMMbZJv8cU8YtPkPbexgrf3J8&usqp=CAU", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7742), new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7742) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { "https://dynamic.brandcrowd.com/template/preview/design/90728fda-b283-4797-973e-9a0775dec439?v=4&designTemplateVersion=5&size=design-preview-standalone-1x", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7744), new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7744) });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo1cHnjZlK64h9Pc5OvWCYfYWYexByKhPpeg&s", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7746), new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7746) });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CoverPicture", "CreationDate", "Description", "Duration", "LessonsNo", "Price", "Rate", "SectionsNo", "Title", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 5, 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCmO_j4YW82XwWIM-_Fo6afxyuN2pSGoZMBQ&s", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7748), null, null, 0, 0m, null, 0, "Alogrithms", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7748), 1 },
                    { 6, 1, "https://d1jnx9ba8s6j9r.cloudfront.net/imgver.1551437392/img/co_img_1539_1633434090.png", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7750), null, null, 0, 0m, null, 0, "Introduction to C++", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7750), 1 },
                    { 7, 1, "https://static.gunnarpeipman.com/wp-content/uploads/2019/12/ef-core-featured.png", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7752), null, null, 0, 0m, null, 0, "EF Core", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7752), 1 },
                    { 8, 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTKIa50KjBUhvtvuMbOaL_QtJrzstWIQA3YSg&s", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7754), null, null, 0, 0m, null, 0, "Database Using SQL Server", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7754), 1 },
                    { 9, 1, "https://www.construx.com/wp-content/uploads/2018/08/design-pattern-essentials-course-image.jpg", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7756), null, null, 0, 0m, null, 0, "Design Pattern", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7756), 1 },
                    { 10, 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-HJM_i7rOg2yY9OgpVPYRLL4fYjA9CTfEoQ&s", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7758), null, null, 0, 0m, null, 0, "SOLID Principle", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7758), 1 },
                    { 11, 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQtx7PjCp_KBWQZtHauOWMG2WiRpXxjpbYf3w&s", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7759), null, null, 0, 0m, null, 0, "How To Train", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7760), 2 },
                    { 12, 2, "https://static.vecteezy.com/system/resources/previews/024/700/836/non_2x/fitness-gym-training-social-media-timeline-cover-and-video-thumbnail-and-web-banner-design-free-vector.jpg", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7761), null, null, 0, 0m, null, 0, "Life Coach", new DateTime(2024, 9, 6, 5, 24, 35, 637, DateTimeKind.Local).AddTicks(7762), 2 }
                });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 3 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 6, 5, 24, 35, 638, DateTimeKind.Local).AddTicks(1124));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 4 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 6, 6, 24, 35, 638, DateTimeKind.Local).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 1,
                column: "SectionNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 2,
                column: "SectionNumber",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 3,
                column: "SectionNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 4,
                column: "SectionNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: 5,
                column: "SectionNumber",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_CourseId",
                table: "Rating",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_StudentId",
                table: "Rating",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "SectionNumber",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ProfilePicture" },
                values: new object[] { "0284a6fa-7dca-4c4e-a646-8bdde79ffbf8", "" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ProfilePicture" },
                values: new object[] { "74994475-d6ea-412e-b377-b37a8e2b988b", "" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fa98f495-bcf7-48d2-aff7-48fb04aa6866");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "259a9296-58d0-440f-ac62-294f17af1b21");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { null, new DateTime(2024, 9, 5, 15, 47, 55, 839, DateTimeKind.Local).AddTicks(364), null });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { null, new DateTime(2024, 9, 5, 15, 47, 55, 839, DateTimeKind.Local).AddTicks(418), null });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { null, new DateTime(2024, 9, 5, 15, 47, 55, 839, DateTimeKind.Local).AddTicks(420), null });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoverPicture", "CreationDate", "UpdatedDate" },
                values: new object[] { null, new DateTime(2024, 9, 5, 15, 47, 55, 839, DateTimeKind.Local).AddTicks(422), null });

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 3 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 5, 15, 47, 55, 839, DateTimeKind.Local).AddTicks(8006));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 4 },
                column: "EnrollmentDate",
                value: new DateTime(2024, 9, 5, 16, 47, 55, 839, DateTimeKind.Local).AddTicks(8028));
        }
    }
}
