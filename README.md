# Разработка корпоративных приложений — лабораторная работа №2

В рамках второй лабораторной работы реализован REST API сервер для управления объектами фитнес-клуба. Серверная часть построена с использованием ASP.NET Core и разделена на слой контроллеров и сервисов. Доменная модель переиспользуется из первой лабораторной работы.

Вариант: **14**

---

## Архитектура проекта

Реализована многослойная архитектура, обеспечивающая связность компонентов:

1. **`FitnessClub.Domain`** — содержит основные сущности бизнес-логики, перечисления, интерфейсы репозиториев, контекст данных в памяти и генератор начальных данных.
2. **`FitnessClub.Contracts`** — содержит DTO-модели, разделенные на Read-модели и Edit-модели с атрибутами валидации, а также интерфейсы сервисов.
3. **`FitnessClub.Application`** — реализует сервисы, содержащие всю логику приложения, маппинг данных и асинхронные операции.
4. **`FitnessClub.Infrastructure.InMemory`** — реализует паттерн для работы с данными в оперативной памяти в качестве подготовки к будущей интеграции с Entity Framework Core.
5. **`FitnessClub.Server`** — контроллеры, обрабатывающие входящие HTTP-запросы, выполняющие логирование и возвращающие стандартизированные ответы.
---

## Внедрение зависимостей

В конфигурации приложения (`Program.cs`) реализован механизм внедрения зависимостей.
Для сохранения состояния данных между HTTP-запросами в оперативной памяти `FitnessClubContext` и все сервисы зарегистрированы с жизненным циклом Singleton:

```csharp
builder.Services.AddSingleton(sp => DataSeeder.Seed());

builder.Services.AddSingleton<IRepository<Client>, ClientRepository>();
builder.Services.AddSingleton<IRepository<Trainer>, TrainerRepository>();
builder.Services.AddSingleton<IRepository<TrainingSession>, TrainingSessionRepository>();
builder.Services.AddSingleton<IRepository<GymHall>, GymHallRepository>();
builder.Services.AddSingleton<IRepository<Specialization>, SpecializationRepository>();

builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ITrainerService, TrainerService>();
builder.Services.AddSingleton<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddSingleton<IGymHallService, GymHallService>();
builder.Services.AddSingleton<ISpecializationService, SpecializationService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();
```

---

## Описание Web API (Эндпоинты)
| Маршрут                         | Метод   | Описание                           |
|---------------------------------|---------|------------------------------------|
| `/api/Clients`                  | GET     | Получить список всех клиентов      | 
|                                 | POST    | Добавить нового клиента            |
| `/api/Clients/{id}`             | GET     | Получить клиента по Id             |
|                                 | PUT     | Обновить данные клиента            |
|                                 | DELETE  | Удалить клиента                    |
| `/api/Trainers`                 | GET     | Получить список всех тренеров      |
|                                 | POST    | Добавить нового тренера            |
| `/api/Trainers/{id}`            | GET     | Получить тренера по Id             |
|                                 | PUT     | Обновить данные тренера            |
|                                 | DELETE  | Удалить тренера                    |
| `/api/GymHalls`                 | GET     | Получить список всех залов         |
|                                 | POST    | Добавить новый зал                 |
| `/api/GymHalls/{id}`            | GET     | Получить зал по Id                 |
|                                 | PUT     | Обновить данные зала               |
|                                 | DELETE  | Удалить зал                        |
| `/api/Specializations`          | GET     | Получить список всех специализаций |
|                                 | POST    | Добавить новую специализацию       |
| `/api/Specializations/{id}`     | GET     | Получить специализацию по Id       |
|                                 | PUT     | Обновить данные специализации      |
|                                 | DELETE  | Удалить специализацию              |
| `/api/TrainingSessions`         | GET     | Получить все тренировки            |
|                                 | POST    | Создать новую тренировку           |
| `/api/TrainingSessions/{id}`    | GET     | Получить тренировку по Id          |
|                                 | PUT     | Обновить тренировку                |
|                                 | DELETE  | Удалить тренировку                 |

### Аналитические запросы
| Маршрут                                  | Метод | Описание                                                                         |
|------------------------------------------|-------|----------------------------------------------------------------------------------|
| `/api/Analytics/experienced-trainers`    | GET   | Выводит тренеров со стажем работы от 5 лет                                       | 
| `/api/Analytics/hall-availability`       | GET   | Проверяет, свободен ли выбранный зал                                             |
| `/api/Analytics/expired-subscriptions`   | GET   | Клиенты с просроченным абонементом на дату                                       |
| `/api/Analytics/sessions-by-hall-month`  | GET   | Тренировки за месяц в конкретном зале                                            |
| `/api/Analytics/top-5-trainers`          | GET   | Топ-5 популярных тренеров по числу занятий, выводит тренеров и кол-во тренировок |

---

## Запуск проекта
- Перейдите в директорию серверного проекта (FitnessClub.Server).
- Запустите приложение командой:
```bash
cd FitnessClub.Server
dotnet run
```
<img width="1080" height="269" alt="image" src="https://github.com/user-attachments/assets/742e2359-663a-44d9-8588-cc6210361452" />