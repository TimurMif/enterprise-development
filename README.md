# Разработка корпоративных приложений — лабораторная работа №2

В рамках второй лабораторной работы реализован REST API сервер для управления объектами фитнес-клуба. Серверная часть построена с использованием ASP.NET Core и разделена на слой контроллеров и сервисов. Доменная модель переиспользуется из первой лабораторной работы.

Вариант: **14**

---

## Архитектура проекта

Реализована многослойная архитектура, обеспечивающая связность компонентов:

1. **Доменная модель (`FitnessClub.Domain`)** — содержит сущности (`Client`, `Trainer`, `TrainingSession`, `Person`), контекст данных (`FitnessClubContext`) и генератор начальных данных (`DataSeeder`).
2. **Слой сервисов (`FitnessClub.Server.Services`)** — инкапсулирует бизнес-логику. Реализованы интерфейсы и классы сервисов для CRUD-операций и сложной аналитики:
    - `ClientService`
    - `TrainerService`
    - `TrainingSessionService`
    - `AnalyticsService`
3. **Слой контроллеров (`FitnessClub.Server.Controllers`)** — обрабатывает входящие HTTP-запросы, вызывает соответствующие сервисы и возвращает результаты клиенту.

---

## Внедрение зависимостей

В конфигурации приложения (`Program.cs`) реализован механизм внедрения зависимостей.
Для сохранения состояния данных между HTTP-запросами в оперативной памяти `FitnessClubContext` и все сервисы зарегистрированы с жизненным циклом Singleton:

```csharp
builder.Services.AddSingleton(sp => DataSeeder.Seed());
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ITrainerService, TrainerService>();
builder.Services.AddSingleton<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();
```

---

## Описание Web API (Эндпоинты)
| Маршрут                      | Метод  | Описание                      |
|------------------------------|--------|-------------------------------|
| `/api/Clients`               | GET    | Получить список всех клиентов | 
|                              | POST   | Добавить нового клиента       |
| `/api/Clients/{id}`          | GET    | Получить клиента по Id        |
|                              | PUT    | Обновить данные клиента       |
|                              | DELETE | Удалить клиента               |
| `/api/Trainers`              | GET    | Получить список всех тренеров |
|                              | POST   | Добавить нового тренера       |
| `/api/Trainers/{id}`         | GET    | Получить тренера по Id        |
|                              | PUT    | Обновить данные тренера       |
|                              | DELETE | Удалить тренера               |
| `/api/TrainingSessions`      | GET    | Получить все тренировки       |
|                              | POST   | Создать новую тренировку      |
| `/api/TrainingSessions/{id}` | GET    | Получить тренировку по Id     |
|                              | PUT    | Обновить тренировку           |
|                              | DELETE | Удалить тренировку            |

### Аналитические запросы
| Маршрут                                  | Метод | Описание                                                  |
|------------------------------------------|-------|-----------------------------------------------------------|
| `/api/Analytics/experienced-trainers`    | GET   | Выводит тренеров со стажем работы от 5 лет                | 
| `/api/Analytics/hall-availability`       | GET   | Проверяет, свободен ли выбранный зал                      |
| `/api/Analytics/expired-subscriptions`   | GET   | Клиенты с просроченным абонементом на дату                |
| `/api/Analytics/sessions-by-hall-month`  | GET   | Тренировки за месяц в конкретном зале                     |
| `/api/Analytics/top-5-trainers`          | GET   | Топ-5 популярных тренеров по числу занятий                |

---

## Запуск проекта
 - Перейдите в директорию серверного проекта (FitnessClub.Server).
 - Запустите приложение командой:
```bash
cd FitnessClub.Server
dotnet run
```
<img width="1080" height="269" alt="image" src="https://github.com/user-attachments/assets/742e2359-663a-44d9-8588-cc6210361452" />



