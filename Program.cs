
using Microsoft.EntityFrameworkCore;
using SiteSurveyApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 PostgreSQL + PostGIS connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Postgres"),
        o => o.UseNetTopologySuite()
    )
);

// 🔹 CORS (HTML / JS access ke liye)
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

// 🔹 Swagger (testing ke liye)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 CORS enable
app.UseCors("AllowAll");

// 🔹 Static files (uploads folder ke liye)
app.UseStaticFiles();

// 🔹 Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// 🔹 API routes
app.MapControllers();

app.Run();
