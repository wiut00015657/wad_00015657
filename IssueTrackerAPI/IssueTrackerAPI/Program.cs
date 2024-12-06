using IssueTrackerAPI.Data;
using IssueTrackerAPI.MappingProfiles;
using IssueTrackerAPI.Models;
using IssueTrackerAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<GeneralDbContext>(o => o.UseSqlServer(
    builder.Configuration.GetConnectionString("DevConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); 
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Issue Tracker API 15657",
        Version = "v1",
        Description = "WAD 00015657"
    });
});

builder.Services.AddTransient<IRepository<Issue>, IssueRepository>();
builder.Services.AddTransient<IRepository<Employee>, EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
options.WithOrigins("http://http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
