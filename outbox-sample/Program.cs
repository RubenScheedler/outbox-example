using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using outbox_sample.Database;
using outbox_sample.Messaging;
using outbox_sample.Messaging.Abstraction;
using outbox_sample.Orders;
using outbox_sample.Orders.Abstraction;
using outbox_sample.Outbox;
using outbox_sample.Outbox.Abstraction;

namespace outbox_sample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options
                .UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
                .UseSnakeCaseNamingConvention()
            );

        builder.Services.AddScoped<DatabaseInitializer>();
        builder.Services.AddScoped<IMessageBroker, MessageBroker>();
        builder.Services.AddScoped<IOutbox, Outbox.Outbox>();
        builder.Services.AddScoped<IPlaceOrderHandler, PlaceOrderHandler>();
        builder.Services.AddHostedService<OutboxProcessor>();
        
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var sqlScript = File.ReadAllText("../Scripts/init.sql");
            dbContext.Database.ExecuteSqlRaw(sqlScript);
        }
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapPost("/place-order", (HttpContext _, PlaceOrder command, [FromServices] IPlaceOrderHandler placeOrderHandler) =>
            {
                placeOrderHandler.Handle(command);
            })
            .WithName("PlaceOrder")
            .WithOpenApi();

        app.Run();
    }
}