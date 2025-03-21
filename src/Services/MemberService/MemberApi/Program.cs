using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Model;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddSingleton(
    new CosmosClient(builder.Configuration["CosmosDb:ConnectionString"]));
// builder.Services.AddScoped<IRepository<Group>, Repository<Group>>();
builder.Services.AddScoped<IRepository<Member>>(service =>
{
    var databaseName = builder.Configuration["CosmosDb:DatabaseName"];
    var cosmosClient = service.GetRequiredService<CosmosClient>();
    return new MemberRepository(cosmosClient, databaseName, "Members");
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