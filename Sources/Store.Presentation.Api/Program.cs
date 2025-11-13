global using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CosmosExceptionsFilter>();
    options.Filters.Add<BusinessExceptionsFilter>();
});

builder.Services
    .AddOpenApi()
    .AddCurrentSolution(builder.Configuration)
    .Configure<ApiBehaviorOptions>(options => options.SuppressMapClientErrors = true);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Store API";
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program;
