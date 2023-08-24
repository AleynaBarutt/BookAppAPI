
using BookApp.Extensions;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.EfCore;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));



builder.Services.AddControllers(config =>
{
    //content negotiation yap�ld�.
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true; //406 NotAcceptable
})
    .AddCustomCsvFormatter() //CSV FORMATINDA �IKTI
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(); // httppatch i�in ekledik.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extensions k�sm�nda bunun conf yapt�k.
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();

//automapper eklentisi yapt�k.
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//middleware extensions
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
