using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PriceCalculatorAPI;
using PriceCalculatorAPI.Factories;
using PriceCalculatorAPI.Middleware;
using PriceCalculatorAPI.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(setUpAction =>
{
    setUpAction.ReturnHttpNotAcceptable = true;
    setUpAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

});

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
        new BadRequestObjectResult(actionContext.ModelState);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<IPriceCalculatorService, PriceCalculatorService>();
builder.Services.AddTransient<IVatValidator, VatValidator>();
builder.Services.AddTransient<IAustriaVatFactory, AustriaVatFactory>();
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
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
