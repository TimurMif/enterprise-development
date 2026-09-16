using FitnessClub.Domain.Context;
using FitnessClub.Domain.Data;

namespace FitnessClub.Tests.Fixtures;

/// <summary>
///     Общий контекст с данными фитнес-клуба для тестов
/// </summary>
public class FitnessClubFixture
{
    /// <summary>
    ///     Запускает сидер при создании фикстуры
    /// </summary>
    public FitnessClubFixture()
    {
        Context = DataSeeder.Seed();
    }

    /// <summary>
    ///     Контекст с тестовыми данными
    /// </summary>
    public FitnessClubContext Context { get; }
}