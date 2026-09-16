using FitnessClub.Domain.Context;
using FitnessClub.Tests.Fixtures;

namespace FitnessClub.Tests;

/// <summary>
///     Unit-тесты для LINQ-запросов к фитнес-клубу
/// </summary>
public class QueriesTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    ///     1. Тренеры со стажем не менее 5 лет
    /// </summary>
    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(0)]
    public void GetTrainersWithExperience_ReturnsCorrectTrainers(int minExperience)
    {
        var context = fixture.Context;

        var result = context.Trainers
            .Where(t => t.ExperienceYears >= minExperience)
            .ToList();

        Assert.All(result, trainer => Assert.True(
            trainer.ExperienceYears >= minExperience,
            $"Тренер '{trainer.LastName} {trainer.FirstName}' имеет стаж {trainer.ExperienceYears}, " +
            $"что меньше требуемого {minExperience}"));

        var excluded = context.Trainers.Except(result).ToList();
        Assert.All(excluded, trainer => Assert.True(
            trainer.ExperienceYears < minExperience,
            $"Тренер '{trainer.LastName} {trainer.FirstName}' со стажем {trainer.ExperienceYears} " +
            $"должен был попасть в выборку"));

        if (minExperience <= 5) Assert.NotEmpty(result);
    }

    /// <summary>
    ///     2. Зал занят в проверяемый момент
    /// </summary>
    [Fact]
    public void IsHallAvailable_WhenTimeSlotIsOccupied_ReturnsFalse()
    {
        var context = fixture.Context;

        var session = context.TrainingSessions.First();
        var checkTime = session.DateTime.AddMinutes(10);

        var isAvailable = IsHallAvailableAt(context, session.GymHallId, checkTime);

        Assert.False(isAvailable);
    }

    /// <summary>
    ///     2. Зал свободен в проверяемый момент
    /// </summary>
    [Fact]
    public void IsHallAvailable_WhenTimeSlotIsFree_ReturnsTrue()
    {
        var context = fixture.Context;
        var hall = context.GymHalls.First();
        var checkTime = DateTime.Now.AddYears(1);
        var isAvailable = IsHallAvailableAt(context, hall.Id, checkTime);
        Assert.True(isAvailable);
    }

    /// <summary>
    ///     Проверяет, свободен ли зал в указанный момент
    /// </summary>
    private static bool IsHallAvailableAt(FitnessClubContext context, Guid hallId, DateTime checkTime)
    {
        var isOccupied = context.TrainingSessions.Any(s =>
            s.GymHallId == hallId &&
            s.DateTime <= checkTime &&
            s.DateTime.Add(s.Duration) > checkTime);

        return !isOccupied;
    }

    /// <summary>
    ///     3. Клиенты с просроченным абонементом, сортировка по фамилии и имени
    /// </summary>
    [Fact]
    public void GetClientsWithExpiredSubscription_SortedByName()
    {
        var context = fixture.Context;
        var today = DateOnly.FromDateTime(DateTime.Now);

        var result = context.Clients
            .Where(c => c.SubscriptionEndDate < today)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.MiddleName)
            .ToList();

        Assert.All(result, client => Assert.True(
            client.SubscriptionEndDate < today,
            $"У клиента '{client.LastName} {client.FirstName}' абонемент до {client.SubscriptionEndDate}, " +
            $"что не является просроченным на {today}"));

        var expectedCount = context.Clients.Count(c => c.SubscriptionEndDate < today);
        Assert.Equal(expectedCount, result.Count);

        for (var i = 1; i < result.Count; i++)
        {
            var prev = $"{result[i - 1].LastName} {result[i - 1].FirstName} {result[i - 1].MiddleName}";
            var curr = $"{result[i].LastName} {result[i].FirstName} {result[i].MiddleName}";
            Assert.True(
                string.Compare(prev, curr, StringComparison.Ordinal) <= 0,
                $"Нарушен порядок сортировки: '{prev}' должен идти после '{curr}'");
        }
    }

    /// <summary>
    ///     4. Занятия за текущий месяц в выбранном зале
    /// </summary>
    [Fact]
    public void GetSessionsForCurrentMonthInSelectedHall_ReturnsFilteredSessions()
    {
        var context = fixture.Context;
        var now = DateTime.Now;

        var targetHall = context.GymHalls.FirstOrDefault(h =>
            context.TrainingSessions.Any(s =>
                s.GymHallId == h.Id &&
                s.DateTime.Year == now.Year &&
                s.DateTime.Month == now.Month));

        Assert.NotNull(targetHall);

        var result = context.TrainingSessions
            .Where(s => s.GymHallId == targetHall.Id
                        && s.DateTime.Year == now.Year
                        && s.DateTime.Month == now.Month)
            .ToList();

        Assert.NotEmpty(result);

        Assert.All(result, session =>
        {
            Assert.Equal(targetHall.Id, session.GymHallId);
            Assert.Equal(now.Year, session.DateTime.Year);
            Assert.Equal(now.Month, session.DateTime.Month);
        });

        var expectedCount = context.TrainingSessions.Count(s =>
            s.GymHallId == targetHall.Id &&
            s.DateTime.Year == now.Year &&
            s.DateTime.Month == now.Month);

        Assert.Equal(expectedCount, result.Count);
    }

    /// <summary>
    ///     5. Топ 5 популярных тренеров
    /// </summary>
    [Fact]
    public void GetTopFivePopularTrainers_ReturnsMaxFiveTrainers()
    {
        var context = fixture.Context;

        var result = context.TrainingSessions
            .GroupBy(s => s.Trainer)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var trainersWithSessions = context.TrainingSessions
            .Select(s => s.Trainer)
            .Distinct()
            .Count();

        Assert.Equal(Math.Min(5, trainersWithSessions), result.Count);

        Assert.Equal(result.Count, result.Distinct().Count());

        var counts = result
            .Select(t => context.TrainingSessions.Count(s => s.Trainer.Id == t.Id))
            .ToList();

        for (var i = 1; i < counts.Count; i++)
            Assert.True(
                counts[i - 1] >= counts[i],
                $"Нарушен порядок: тренер на позиции {i - 1} имеет {counts[i - 1]} занятий, " +
                $"а на позиции {i} — {counts[i]}");

        var maxCount = context.TrainingSessions
            .GroupBy(s => s.Trainer)
            .Max(g => g.Count());

        Assert.Equal(maxCount, counts[0]);
    }
}