
using BookApp.Extensions;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.EfCore;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));



builder.Services.AddControllers(config =>
{
    //content negotiation yapýldý.
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true; //406 NotAcceptable
})
    .AddCustomCsvFormatter() //CSV FORMATINDA ÇIKTI
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(); // httppatch için ekledik.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extensions kýsmýnda bunun conf yaptýk.
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();

//automapper eklentisi yaptýk.
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
