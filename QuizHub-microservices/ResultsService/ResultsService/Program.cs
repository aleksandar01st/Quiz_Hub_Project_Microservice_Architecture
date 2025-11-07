using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ResultsService.Data;
using ResultsService.Repository;
using ResultsService.Service;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// DB context
builder.Services.AddDbContext<ResultsServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ResultConnection")));

// Repo i servisi
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IResultService, ResultService>();

// HttpClient za komunikaciju sa drugim servisima
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
