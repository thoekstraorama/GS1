using GS1.ServiceDefaults;
using Microsoft.AspNetCore.HttpLogging;
using GS1.ApiService.Features.Items;
using GS1.ApiService.Services;
using GS1.ApiService;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
});

builder.Services.AddHandlers(typeof(Program).Assembly);
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddGS1DbContext(builder.Configuration);

var app = builder.Build();

await app.ApplyMigrations();

await app.SeedDatabase();

app.UseHttpLogging();
app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseHealthChecks("/health");

/*
 * Hier moet nog authenticatie / autorisatie toegevoegd worden.
 * De plaatsing in de request pipeline is afhankelijk van of de health endpoints bijv publiek beschikbaar moeten zijn.
*/

app.MapDefaultEndpoints();

app.MapItemsEndpoints();

app.Run();
