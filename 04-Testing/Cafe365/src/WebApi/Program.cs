using Cafe365.Application;
using Cafe365.Infrastructure;
using Cafe365.Infrastructure.Persistence;
using Cafe365.WebApi;
using Cafe365.WebApi.Features;
using Cafe365.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initializer.InitializeAsync();
    await initializer.SeedAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi3(settings => settings.DocumentPath = "/api/specification.json");

app.UseRouting();

app.UseExceptionFilter();

app.MapCustomerEndpoints();
app.MapOrderEndpoints();

app.Run();

public partial class Program { }