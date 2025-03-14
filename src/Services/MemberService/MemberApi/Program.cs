using Database.Repositories;
using Database.Repositories.Interfaces;
using DataLayer.Repositories;
using Microsoft.Azure.Cosmos;
using Model;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<Group>, Repository<Group>>();
builder.Services.AddScoped<IRepository<Member>,MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddSingleton(
    new CosmosClient(builder.Configuration.GetConnectionString("CosmosDb")));

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