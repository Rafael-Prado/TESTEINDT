using DomainProposta.Config;
using DomainProposta.Interfaces;
using DomainProposta.Services;
using InfraProposta.Data;
using InfraProposta.Kafka;
using InfraProposta.Mongo;
using InfraProposta.Repositories;
using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();
builder.Services.AddSingleton<IEventConsumer, KafkaEventConsumer>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Migrations (ou criação de tabelas)
var migration = new MigrationService(builder.Configuration.GetConnectionString("DefaultConnection"));
migration.Run();

BsonSerializer.RegisterSerializer(
    new GuidSerializer(GuidRepresentation.Standard)
); ;

builder.Services.AddSingleton<IMongoService>(provider =>
    new MongoService(builder.Configuration["Mongo:ConnectionString"], "Seguro"));


builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("KafkaSettings"));

builder.Services.AddHostedService<KafkaConsumerHostedService>();

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
