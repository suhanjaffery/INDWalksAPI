using IndWalksAPI.Data;
using IndWalksAPI.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using IndWalksAPI.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<INDWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("INDWalksConnectionString")));
builder.Services.AddScoped<IndWalksAPI.Repo.IRegionRepo, IndWalksAPI.Repo.SqlRegionRepo>();
builder.Services.AddScoped<IRegionRepo, SqlRegionRepo>();
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
