## Разработать API для управления данными о сотрудниках и проектах компании.

## API должен поддерживать следующие операции:

- Добавление подразделения компании (наименование не более 100 символов)
- Добавление, удаление сотрудника (имя (не более 30 символов), фамилия (не более 50 символов), пол, подразделение компании)
- Возможность загрузки и обновления фотографии сотрудника
- Обновление информации о сотруднике (имя, фамилия, подразделение)
- Предоставление списка сотрудников компании
- Предоставление ссылки на просмотр фотографии сотрудника
- Создание, обновление данных о проектах компании (наименование проекта, бюджет)
- Предоставление списка проектов компании
- Добавление сотрудника в проект (сотрудник может участвовать в нескольких проектах)
- Удаление сотрудника из проекта
- Предоставление списка всех сотрудников запрашиваемого проекта
 
## Технические ограничения:
- API реализовать с использованием технологии ASP.NET Core1 WebApi (https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1)
- Предоставление списка проектов и списка сотрудников обеспечить постранично
- Использовать IoC контейнер Autofac (https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting)
- Хранение данных обеспечить с использованием Microsoft SQL Server
- Операции с базой данных осуществлять с использованием Entity Framework Core (https://docs.microsoft.com/en-us/ef/core/)
- Для работы с EF Core реализовать Generic Repository (https://garywoodfine.com/generic-repository-pattern-net-core/)
- Обеспечить логгирование в файл с использованием Serilog (https://serilog.net/)
- Интеграцию Serilog с WebApi осуществить с применением Microsoft.Extensions.Logging (https://github.com/serilog/serilog-extensions-logging)
- Обеспечить логгирование Exceptions в WebApi
- Минимальный уровень логгирования а также CORS (https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1) конфигурировать через appsettings.json и переменные среды (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#configureappconfiguration)
- Обеспечить валидацию данных с использованием Fluent Validation (https://fluentvalidation.net/)
- Интегрировать Swagger и SwaggerUI в WebApi для возможности тестирования приложения (https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio)
- Для хранения изображений, генерации ссылок на их загрузку и скачивание использовать отдельный Blob Storage. (https://github.com/minio/minio-dotnet)
- Добавить и настроить StyleCop Analyzer во все создаваемые .NET Core/Standard проекты (https://github.com/DotNetAnalyzers/StyleCopAnalyzers)