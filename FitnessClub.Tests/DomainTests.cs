using FitnessClub.Tests.Fixtures;

namespace FitnessClub.Tests;

/// <summary>
///     Тесты проверки данных и связей доменной модели
/// </summary>
public class DomainTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    ///     Сидер создаёт минимум по 10 экземпляров каждого класса
    /// </summary>
    [Fact]
    public void SeederShouldCreateEnoughData()
    {
        var context = fixture.Context;

        Assert.True(context.Specializations.Count >= 10,
            $"Специализаций: {context.Specializations.Count}, ожидалось ≥ 10");
        Assert.True(context.GymHalls.Count >= 10,
            $"Залов: {context.GymHalls.Count}, ожидалось ≥ 10");
        Assert.True(context.Clients.Count >= 10,
            $"Клиентов: {context.Clients.Count}, ожидалось ≥ 10");
        Assert.True(context.Trainers.Count >= 10,
            $"Тренеров: {context.Trainers.Count}, ожидалось ≥ 10");
        Assert.True(context.TrainingSessions.Count >= 10,
            $"Занятий: {context.TrainingSessions.Count}, ожидалось ≥ 10");
    }

    /// <summary>
    ///     Проверка того, что у каждого тренера назначена специализация
    /// </summary>
    [Fact]
    public void SeederShouldCreateTrainersWithSpecializations()
    {
        var context = fixture.Context;

        Assert.All(context.Trainers, trainer =>
        {
            Assert.NotNull(trainer.Specialization);
            Assert.NotEqual(Guid.Empty, trainer.SpecializationId);
            Assert.Equal(trainer.Specialization.Id, trainer.SpecializationId);
        });
    }

    /// <summary>
    ///     У каждого занятия есть клиент, тренер и зал
    /// </summary>
    [Fact]
    public void SeederShouldCreateTrainingSessionsWithRelations()
    {
        var context = fixture.Context;

        Assert.All(context.TrainingSessions, session =>
        {
            Assert.NotNull(session.Client);
            Assert.NotNull(session.Trainer);
            Assert.NotNull(session.GymHall);

            Assert.NotEqual(Guid.Empty, session.ClientId);
            Assert.NotEqual(Guid.Empty, session.TrainerId);
            Assert.NotEqual(Guid.Empty, session.GymHallId);

            Assert.Equal(session.Client.Id, session.ClientId);
            Assert.Equal(session.Trainer.Id, session.TrainerId);
            Assert.Equal(session.GymHall.Id, session.GymHallId);
        });
    }

    /// <summary>
    ///     Проверка, что дата окончания абонемента не раньше даты начала
    /// </summary>
    [Fact]
    public void ClientsSubscriptionEndDateShouldBeAfterStartDate()
    {
        var context = fixture.Context;

        Assert.All(context.Clients, client => Assert.True(
            client.SubscriptionEndDate >= client.SubscriptionStartDate,
            $"У клиента '{client.LastName} {client.FirstName}' окончание абонемента ({client.SubscriptionEndDate}) " +
            $"раньше начала ({client.SubscriptionStartDate})"));
    }

    /// <summary>
    ///     У занятия заполнены поля контракта: дата, зал, пробное посещение
    /// </summary>
    [Fact]
    public void TrainingSessionShouldContainContractFields()
    {
        var context = fixture.Context;

        Assert.All(context.TrainingSessions, session =>
        {
            Assert.NotEqual(default, session.DateTime);
            Assert.True(session.Duration > TimeSpan.Zero);
            Assert.NotNull(session.GymHall);
            Assert.False(string.IsNullOrWhiteSpace(session.GymHall.Name));
        });

        Assert.Contains(context.TrainingSessions, s => s.IsTrial);
        Assert.Contains(context.TrainingSessions, s => !s.IsTrial);
    }

    /// <summary>
    ///     Проверка уникальности идентификаторов у всех сущностей
    /// </summary>
    [Fact]
    public void AllEntitiesShouldHaveUniqueIds()
    {
        var context = fixture.Context;

        AssertUniqueIds(context.Clients.Select(c => c.Id), nameof(context.Clients));
        AssertUniqueIds(context.Trainers.Select(t => t.Id), nameof(context.Trainers));
        AssertUniqueIds(context.GymHalls.Select(h => h.Id), nameof(context.GymHalls));
        AssertUniqueIds(context.Specializations.Select(s => s.Id), nameof(context.Specializations));
        AssertUniqueIds(context.TrainingSessions.Select(s => s.Id), nameof(context.TrainingSessions));
    }

    private static void AssertUniqueIds(IEnumerable<Guid> ids, string entityName)
    {
        var list = ids.ToList();
        Assert.Equal(list.Count, list.Distinct().Count());
        Assert.All(list, id => Assert.NotEqual(Guid.Empty, id));
    }
}
