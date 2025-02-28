using Database.Repositories;
using Database.Repositories.Interfaces;
using Model;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<Group>, Repository<Group>>(); 
builder.Services.AddScoped<IRepository<Member>, Repository<Member>>(); 
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IGroupService, ClubService>();

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