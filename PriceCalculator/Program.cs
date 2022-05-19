using Microsoft.AspNetCore.Mvc.Formatters;
using PriceCalculatorAPI;
using PriceCalculatorAPI.Middleware;
using PriceCalculatorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( setUpAction => 
{
    setUpAction.ReturnHttpNotAcceptable = true;
    setUpAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
   


});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<IPriceCalculatorService, PriceCalculatorService>();
builder.Services.AddTransient<IVATValidator, VATValidator>();
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
