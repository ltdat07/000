using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Domain Repositories
using Zoo_Dz.Domain.Repositories;

// Infrastructure Implementations
using Zoo_Dz.Infrastructure.Repositories;
using Zoo_Dz.Infrastructure.Events;

// Application Services & Ports
using Zoo_Dz.Application.Services;
using Zoo_Dz.Application.Ports.Events;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем MVC-контроллеры
builder.Services.AddControllers();

// 2. Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Регистрация In-Memory репозиториев
builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

// 4. Регистрация сервисов Application
builder.Services.AddScoped<IAnimalTransferService, AnimalTransferService>();
builder.Services.AddScoped<IFeedingOrganizationService, FeedingOrganizationService>();
// Позже: builder.Services.AddScoped<IZooStatisticsService, ZooStatisticsService>();

// 5. Паблишер доменных событий
builder.Services.AddSingleton<IDomainEventPublisher, SimpleDomainEventPublisher>();

var app = builder.Build();

// 6. Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 7. Маршрутизация на контроллеры
app.MapControllers();

app.Run();
