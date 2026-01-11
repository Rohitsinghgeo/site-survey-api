using Microsoft.EntityFrameworkCore;
using SiteSurveyApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 PostgreSQL + PostGIS
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseNetTopologySuite()
    )
);

// 🔹 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

// 🔹 Controllers
builder.Services.AddControllers();

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 CORS
app.UseCors("AllowAll");

// 🔹 Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 🔹 Routes
app.MapControllers();

app.Run();
