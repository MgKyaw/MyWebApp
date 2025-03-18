using Microsoft.AspNetCore.Rewrite;
using MyWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WelcomeService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    await next(); 
    Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
});

app.UseRewriter(new RewriteOptions().AddRedirect("history", "about"));

app.MapGet("/", (WelcomeService welcomeService) => welcomeService.GetWelcomeMessage());
app.MapGet("/about", () => "Contoso was founded in 2000.");

app.Run();
