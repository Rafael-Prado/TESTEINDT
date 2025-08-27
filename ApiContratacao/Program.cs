using ApiContratacao.Commands.Handlers;
using DomainContratacao.Interfaces;
using InfraContratacao.Cache;
using InfraContratacao.Kafka;
using InfraContratacao.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IContratacaoRepository, ContratacaoRepository>();


builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configuração do Kafka Event Bus
builder.Services.AddSingleton<IEventBus>(provider =>
    new KafkaEventBus(builder.Configuration["Kafka:ConnectionString"], "proposta-contratada-topic"));

builder.Services.AddSingleton<ICacheService>(provider =>
    new RedisCacheService(builder.Configuration["Redis:ConnectionString"]));

builder.Services.AddHostedService<PropostaCacheBackgroundService>();

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
