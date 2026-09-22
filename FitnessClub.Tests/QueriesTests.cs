using FitnessClub.Domain.Data;
using FitnessClub.Tests.Fixtures;

namespace FitnessClub.Tests;

/// <summary>
/// Unit-тесты для LINQ запросов к фитнес-клубу
/// </summary>
public class QueriesTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    /// Тренеры со стажем не менее 5 лет
    /// </summary>
    [Fact]
    public void GetTrainersWithExperience_ReturnsCorrectTrainers()
    {
        var context = fixture.Context;

        var result = context.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .Select(t => t.LastName)
            .ToList();

        var expected = new[]
        {
            "Орлова", "Фёдоров", "Михайлова", "Белов",
            "Григорьева", "Тихонов", "Егорова"
        };

        Assert.Equal(expected.Length, result.Count);
        Assert.All(expected, name => Assert.Contains(name, result));
    }

    /// <summary>
    /// Занят ли зал в проверяемый момент
    /// Занятие 1: Зал Йоги 05.06.2025 с 10:00 до 11:00
    /// </summary>
    [Fact]
    public void IsHallAvailable_WhenTimeSlotIsOccupied_ReturnsFalse()
    {
        var context = fixture.Context;
        const int hallId = 1;
        var checkTime = new DateTime(2025, 6, 5, 10, 30, 0);

        var isOccupied = context.TrainingSessions.Any(s =>
            s.GymHallId == hallId &&
            s.DateTime <= checkTime &&
            s.DateTime.Add(s.Duration) > checkTime);

        Assert.True(isOccupied);
    }

    /// <summary>
    /// Свободен ли зал в проверяемый момент.
    /// В Зале Йоги 05.06.2025 после 11:00 занятий нет.
    /// </summary>
    [Fact]
    public void IsHallAvailable_WhenTimeSlotIsFree_ReturnsTrue()
    {
        var context = fixture.Context;
        const int hallId = 1;
        var checkTime = new DateTime(2025, 6, 5, 12, 0, 0);

        var isOccupied = context.TrainingSessions.Any(s =>
            s.GymHallId == hallId &&
            s.DateTime <= checkTime &&
            s.DateTime.Add(s.Duration) > checkTime);

        Assert.False(isOccupied);
    }

    /// <summary>
    /// Клиенты с просроченным абонементом, сортировка по ФИО
    /// Опорная дата 15.06.2025
    /// </summary>
    [Fact]
    public void GetClientsWithExpiredSubscription_SortedByName_ReturnsExpected()
    {
        var context = fixture.Context;
        var today = DataSeeder.ReferenceToday;

        var result = context.Clients
            .Where(c => c.SubscriptionEndDate < today)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.MiddleName)
            .Select(c => c.LastName)
            .ToList();

        var expected = new[]
        {
            "Кузнецова", "Лебедев", "Попов", "Смирнов", "Соколова"
        };

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Занятия за текущий месяц в зале Йоги.
    /// </summary>
    [Fact]
    public void GetSessionsForJuneInSelectedHall_Returns4Sessions()
    {
        var context = fixture.Context;
        const int hallId = 1;
        var year = DataSeeder.ReferenceDate.Year;
        var month = DataSeeder.ReferenceDate.Month;

        var result = context.TrainingSessions
            .Where(s => s.GymHallId == hallId
                        && s.DateTime.Year == year
                        && s.DateTime.Month == month)
            .Select(s => s.Id)
            .OrderBy(id => id)
            .ToList();

        var expected = new[] { 1, 2, 3, 4 };

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Топ-5 популярных тренеров.
    /// </summary>
    [Fact]
    public void GetTopFivePopularTrainers_ReturnsExpectedOrder()
    {
        var context = fixture.Context;

        var result = context.TrainingSessions
            .GroupBy(s => s.TrainerId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var expected = new[] { 10, 9, 8, 7, 6 };

        Assert.Equal(expected, result);
    }
}