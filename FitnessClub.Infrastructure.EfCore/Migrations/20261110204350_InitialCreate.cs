using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessClub.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subscription_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    subscription_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    passport_number = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gym_halls",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gym_halls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specializations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specializations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    specialization_id = table.Column<int>(type: "integer", nullable: false),
                    experience_years = table.Column<int>(type: "integer", nullable: false),
                    passport_number = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainers", x => x.id);
                    table.ForeignKey(
                        name: "FK_trainers_specializations_specialization_id",
                        column: x => x.specialization_id,
                        principalTable: "specializations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "training_sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    trainer_id = table.Column<int>(type: "integer", nullable: false),
                    gym_hall_id = table.Column<int>(type: "integer", nullable: false),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    trial = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_training_sessions_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_training_sessions_gym_halls_gym_hall_id",
                        column: x => x.gym_hall_id,
                        principalTable: "gym_halls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_training_sessions_trainers_trainer_id",
                        column: x => x.trainer_id,
                        principalTable: "trainers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "date_of_birth", "first_name", "gender", "last_name", "middle_name", "passport_number", "phone_number", "subscription_end_date", "subscription_start_date" },
                values: new object[,]
                {
                    { 1, new DateOnly(1985, 1, 1), "Алексей", 1, "Смирнов", "Иванович", "4000 100000", "+7 900 100-10-20", new DateOnly(2025, 5, 1), new DateOnly(2024, 6, 1) },
                    { 2, new DateOnly(1986, 2, 2), "Мария", 2, "Кузнецова", "Петровна", "4000 100001", "+7 900 101-11-21", new DateOnly(2025, 6, 1), new DateOnly(2024, 7, 15) },
                    { 3, new DateOnly(1987, 3, 3), "Дмитрий", 1, "Попов", "Сергеевич", "4000 100002", "+7 900 102-12-22", new DateOnly(2025, 5, 20), new DateOnly(2024, 8, 10) },
                    { 4, new DateOnly(1988, 4, 4), "Анна", 2, "Соколова", "Владимировна", "4000 100003", "+7 900 103-13-23", new DateOnly(2025, 4, 30), new DateOnly(2024, 9, 1) },
                    { 5, new DateOnly(1989, 5, 5), "Никита", 1, "Лебедев", "Андреевич", "4000 100004", "+7 900 104-14-24", new DateOnly(2025, 6, 10), new DateOnly(2024, 10, 1) },
                    { 6, new DateOnly(1990, 6, 6), "Артём", 1, "Козлов", "Максимович", "4000 100005", "+7 900 105-15-25", new DateOnly(2025, 12, 31), new DateOnly(2025, 1, 1) },
                    { 7, new DateOnly(1991, 7, 7), "Екатерина", 2, "Новикова", "Дмитриевна", "4000 100006", "+7 900 106-16-26", new DateOnly(2026, 2, 1), new DateOnly(2025, 2, 1) },
                    { 8, new DateOnly(1992, 8, 8), "Егор", 1, "Морозов", "Алексеевич", "4000 100007", "+7 900 107-17-27", new DateOnly(2026, 3, 1), new DateOnly(2025, 3, 1) },
                    { 9, new DateOnly(1993, 9, 9), "Ольга", 2, "Петрова", "Николаевна", "4000 100008", "+7 900 108-18-28", new DateOnly(2026, 4, 1), new DateOnly(2025, 4, 1) },
                    { 10, new DateOnly(1994, 10, 10), "Илья", 1, "Волков", "Романович", "4000 100009", "+7 900 109-19-29", new DateOnly(2026, 5, 1), new DateOnly(2025, 5, 1) },
                    { 11, new DateOnly(1995, 11, 11), "Дарья", 2, "Соловьёва", "Кирилловна", "4000 100010", "+7 900 110-20-30", new DateOnly(2025, 12, 1), new DateOnly(2024, 12, 1) },
                    { 12, new DateOnly(1996, 12, 12), "Максим", 1, "Васильев", "Тимурович", "4000 100011", "+7 900 111-21-31", new DateOnly(2026, 1, 15), new DateOnly(2025, 1, 15) },
                    { 13, new DateOnly(1997, 1, 13), "Виктория", 2, "Зайцева", "Андреевна", "4000 100012", "+7 900 112-22-32", new DateOnly(2026, 2, 15), new DateOnly(2025, 2, 15) },
                    { 14, new DateOnly(1998, 2, 14), "Кирилл", 1, "Павлов", "Денисович", "4000 100013", "+7 900 113-23-33", new DateOnly(2026, 3, 15), new DateOnly(2025, 3, 15) },
                    { 15, new DateOnly(1999, 3, 15), "Алиса", 2, "Семёнова", "Артёмовна", "4000 100014", "+7 900 114-24-34", new DateOnly(2026, 4, 15), new DateOnly(2025, 4, 15) }
                });

            migrationBuilder.InsertData(
                table: "gym_halls",
                columns: new[] { "id", "capacity", "name" },
                values: new object[,]
                {
                    { 1, 15, "Зал Йоги" },
                    { 2, 30, "Зал Кроссфита" },
                    { 3, 40, "Зал Кардио" },
                    { 4, 25, "Зал Силовой" },
                    { 5, 20, "Зал Единоборств" },
                    { 6, 15, "Зал Пилатеса" },
                    { 7, 35, "Зал Аэробики" },
                    { 8, 12, "Зал Стретчинга" },
                    { 9, 10, "Зал Реабилитации" },
                    { 10, 50, "Универсальный зал" }
                });

            migrationBuilder.InsertData(
                table: "specializations",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Силовой тренинг" },
                    { 2, "Кроссфит" },
                    { 3, "Пилатес" },
                    { 4, "Йога" },
                    { 5, "Кардио" },
                    { 6, "Единоборства" },
                    { 7, "Тяжелая атлетика" },
                    { 8, "Стретчинг" },
                    { 9, "Реабилитация" },
                    { 10, "Аэробика" }
                });

            migrationBuilder.InsertData(
                table: "trainers",
                columns: new[] { "id", "date_of_birth", "experience_years", "first_name", "gender", "last_name", "middle_name", "passport_number", "phone_number", "specialization_id" },
                values: new object[,]
                {
                    { 1, new DateOnly(1975, 1, 1), 1, "Пётр", 1, "Иванов", "Сергеевич", "4001 200000", "+7 900 200-10-30", 1 },
                    { 2, new DateOnly(1976, 2, 2), 3, "Иван", 1, "Петров", "Алексеевич", "4001 200001", "+7 900 201-11-31", 2 },
                    { 3, new DateOnly(1977, 3, 3), 4, "Николай", 1, "Сидоров", "Михайлович", "4001 200002", "+7 900 202-12-32", 3 },
                    { 4, new DateOnly(1978, 4, 4), 5, "Елена", 2, "Орлова", "Владимировна", "4001 200003", "+7 900 203-13-33", 4 },
                    { 5, new DateOnly(1979, 5, 5), 7, "Андрей", 1, "Фёдоров", "Игоревич", "4001 200004", "+7 900 204-14-34", 5 },
                    { 6, new DateOnly(1980, 6, 6), 8, "Татьяна", 2, "Михайлова", "Юрьевна", "4001 200005", "+7 900 205-15-35", 6 },
                    { 7, new DateOnly(1981, 7, 7), 10, "Виктор", 1, "Белов", "Станиславович", "4001 200006", "+7 900 206-16-36", 7 },
                    { 8, new DateOnly(1982, 8, 8), 12, "Наталья", 2, "Григорьева", "Олеговна", "4001 200007", "+7 900 207-17-37", 8 },
                    { 9, new DateOnly(1983, 9, 9), 13, "Роман", 1, "Тихонов", "Валерьевич", "4001 200008", "+7 900 208-18-38", 9 },
                    { 10, new DateOnly(1984, 10, 10), 15, "Ирина", 2, "Егорова", "Борисовна", "4001 200009", "+7 900 209-19-39", 10 }
                });

            migrationBuilder.InsertData(
                table: "training_sessions",
                columns: new[] { "id", "client_id", "date_start", "duration", "gym_hall_id", "trial", "trainer_id" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 1, false, 10 },
                    { 2, 2, new DateTime(2025, 6, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 1, true, 10 },
                    { 3, 3, new DateTime(2025, 6, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 1, false, 9 },
                    { 4, 4, new DateTime(2025, 6, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 1, false, 10 },
                    { 5, 5, new DateTime(2025, 6, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 2, false, 10 },
                    { 6, 6, new DateTime(2025, 6, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 2, false, 9 },
                    { 7, 7, new DateTime(2025, 6, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 2, true, 9 },
                    { 8, 8, new DateTime(2025, 6, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 3, false, 9 },
                    { 9, 9, new DateTime(2025, 6, 14, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 3, false, 8 },
                    { 10, 10, new DateTime(2025, 6, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 3, false, 8 },
                    { 11, 11, new DateTime(2025, 6, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 4, false, 8 },
                    { 12, 12, new DateTime(2025, 6, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 4, false, 8 },
                    { 13, 13, new DateTime(2025, 6, 9, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 5, false, 7 },
                    { 14, 14, new DateTime(2025, 6, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 5, true, 7 },
                    { 15, 15, new DateTime(2025, 6, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 5, false, 7 },
                    { 16, 1, new DateTime(2025, 6, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 6, false, 6 },
                    { 17, 2, new DateTime(2025, 6, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 6, false, 6 },
                    { 18, 3, new DateTime(2025, 6, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 7, false, 5 },
                    { 19, 4, new DateTime(2025, 5, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 7, false, 9 },
                    { 20, 5, new DateTime(2025, 6, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 8, false, 4 },
                    { 21, 6, new DateTime(2025, 6, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 9, false, 10 },
                    { 22, 7, new DateTime(2025, 7, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0), 10, false, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_trainers_specialization_id",
                table: "trainers",
                column: "specialization_id");

            migrationBuilder.CreateIndex(
                name: "IX_training_sessions_client_id",
                table: "training_sessions",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_training_sessions_gym_hall_id",
                table: "training_sessions",
                column: "gym_hall_id");

            migrationBuilder.CreateIndex(
                name: "IX_training_sessions_trainer_id",
                table: "training_sessions",
                column: "trainer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "training_sessions");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "gym_halls");

            migrationBuilder.DropTable(
                name: "trainers");

            migrationBuilder.DropTable(
                name: "specializations");
        }
    }
}
