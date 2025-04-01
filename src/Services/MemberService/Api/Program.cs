using Application.Interfaces;
using Application.Services;
using Domain;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddSingleton(
    new CosmosClient(builder.Configuration["CosmosDb:ConnectionString"], new CosmosClientOptions()
    {
        SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    }));
builder.Services.AddScoped<IRepository<Member>>(service =>
{
    var databaseName = builder.Configuration["CosmosDb:DatabaseName"];
    var cosmosClient = service.GetRequiredService<CosmosClient>();
    var logger = service.GetRequiredService<ILogger<IRepository<Member>>>(); 
    return new MemberRepository(cosmosClient, databaseName, "Members", logger);
});

// Register the CosmosDbInitializer.
builder.Services.AddSingleton<CosmosDbInitializer>();


var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ensure the database and containers are created before the app starts handling requests.
using (var scope = app.Services.CreateScope())
{
    var cosmosInitializer = scope.ServiceProvider.GetRequiredService<CosmosDbInitializer>();
    await cosmosInitializer.EnsureDatabaseAndContainersExistAsync();
}

app.Run();