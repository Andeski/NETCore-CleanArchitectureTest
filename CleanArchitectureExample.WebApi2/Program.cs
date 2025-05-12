using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Application.Services;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Infrastructure.Data;
using CleanArchitectureExample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureExample.Domain.Entities;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGreetingService, GreetingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("CleanArchitectureDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

builder.Services.AddLogging();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Example users
    if (!dbContext.Users.Any())
    {
        dbContext.Users.AddRange(
            new User { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" },
            new User { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane.smith@example.com" }
        );
        await dbContext.SaveChangesAsync();
    }

    var users = await dbContext.Users.ToListAsync();
    foreach (var user in users)
    {
        logger.LogInformation($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}");
    }
}

app.Run();