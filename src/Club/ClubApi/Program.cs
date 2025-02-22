using Database;
using Model;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<ICosmosDb<Club>, CosmosDb<Club>>(); 
builder.Services.AddScoped<ICosmosDb<Player>, CosmosDb<Player>>(); 
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IClubService, ClubService>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.Run();