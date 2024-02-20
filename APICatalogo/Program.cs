using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Repositories.Implements;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

var valor1 = builder.Configuration["chave1"];
var valor2 = builder.Configuration["secao1:chave2"];

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(mysqlConnection,ServerVersion.AutoDetect(mysqlConnection)));

// Registrando o serviço do filtro
builder.Services.AddScoped<ApiLoggingFilter>();

// Registrando o repositório de categoria
builder.Services.AddScoped<ICategoriaRepository,CategoriaRepository>();

// Registrando o repositório de produto
builder.Services.AddScoped<IProdutoRepository,ProdutoRepository>();

// Registrando o repositório genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Registrando o serviço de UnitOfWork
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

// Registrando o serviço de AutoMapper
builder.Services.AddAutoMapper(typeof(ProdutoDTOMappingProfile));

// Registrando o serviço de logger
//builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
//{
//    LogLevel = LogLevel.Information
//}));

// Registrando o filtro global para todos os controladores
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiLoggingFilter));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    // adicionar o código antes do request
    await next(context);
    // adicionar o código depois do request
});

app.MapControllers();

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware final");
//});

app.Run();
