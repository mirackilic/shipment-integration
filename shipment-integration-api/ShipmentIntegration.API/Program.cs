using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ShipmentIntegration.Application.IServices;
using ShipmentIntegration.Application.Services;
using ShipmentIntegration.Domain.Context;
using ShipmentIntegration.Domain.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IShipmentIntegrationContext, ShipmentIntegrationContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ShipmentIntegrationContext>((optionBuilder) =>
optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ShipmentIntegrationContext))));

builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
