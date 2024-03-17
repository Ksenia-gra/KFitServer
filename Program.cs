using KFitServer.BusinessLogic.Helpers;
using KFitServer.BusinessLogic.Repository;
using KFitServer.BusinessLogic.Services;
using KFitServer.DBContext;
using KFitServer.DBContext.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KfitContext>();
builder.Services.AddSingleton<IHashCreator, BCryptCreator>();
builder.Services.AddScoped<IDbRepository,DbRepository>();
builder.Services.AddScoped<IUserRepository,UserDbRepository>();
builder.Services.AddScoped<INutritionRepository, NutritionDbRepository>();
builder.Services.AddTransient<AuthentificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
