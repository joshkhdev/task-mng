using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Utils.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "TaskManager.AuthCookieAspNetCore";
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DescribeAllParametersInCamelCase();
});
builder.Services.AddCors();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<TaskManagerDbContext>(contextOptions =>
{
    var connectionString = builder.Configuration.GetConnectionString("Db");

    contextOptions.UseSqlite(
        connectionString,
        options => options.MigrationsHistoryTable("__EFMigrationsHistory"));
});

builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<TasksService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "api";
    });
}
else if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
