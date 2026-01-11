
using Microsoft.EntityFrameworkCore;
using SiteSurveyApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 PostgreSQL + PostGIS (NetTopologySuite) connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
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

// 🔹 Swagger (API testing ke liye)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Development me Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Production me bhi Swagger chahiye ho to uncomment kar sakte ho
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 HTTPS redirect
app.UseHttpsRedirection();

// 🔹 CORS enable
app.UseCors("AllowAll");

// 🔹 Static files (uploads folder ke liye)
app.UseStaticFiles();

// 🔹 Authorization (agar future me auth add karo)
app.UseAuthorization();

// 🔹 API routes
app.MapControllers();

app.Run();
