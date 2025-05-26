using Microsoft.EntityFrameworkCore;
using DHouseMvp.Infrastructure.Data;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.Services;
// using AutoMapper; // No longer needed if AddAutoMapper is used with assembly scanning
// using System.Reflection; // No longer needed if AddAutoMapper is used with assembly scanning
using Microsoft.OpenApi.Models;
using DHouseMvp.Application.Mappings; // If you created MappingProfile.cs

// For a custom error handler (Example)
// using DHouseMvp.API.Middleware; // Assuming you create an ErrorHandlingMiddleware

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MVC Core + ApiExplorer (Controllers support)
// builder.Services.AddControllers(); // Using AddMvcCore().AddApiExplorer() is also fine. AddControllers() brings more features by default.
builder.Services
    .AddMvcCore()
    .AddApiExplorer();

// Load connection string
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure EF Core with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlite(conn));

// Register Application Services
builder.Services.AddScoped<IImovelService, ImovelService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

// Configure AutoMapper
// This will scan all assemblies in the current domain, including where MappingProfile might be.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Or, if you prefer to specify the assembly containing your profiles (e.g., where MappingProfile is):
// builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// API Explorer for Minimal APIs (already included with AddApiExplorer for controllers, but harmless)
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DHouseMvp API", Version = "v1" });
    // To include XML comments from your controllers and DTOs (optional):
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // if (File.Exists(xmlPath))
    // {
    //     c.IncludeXmlComments(xmlPath);
    // }
});

var app = builder.Build();

// Apply database migrations automatically on startup
// This is good for development. For production, consider a more controlled strategy.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
        // Optionally, throw the exception if you want the app to fail startup on migration error
        // throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DHouseMvp API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}
else
{
    // For production, you might want a custom exception handler
    // app.UseMiddleware<ErrorHandlingMiddleware>(); // Example
    app.UseExceptionHandler("/Error"); // Or a generic error handler page
    app.UseHsts(); // Adds Strict-Transport-Security header
}

app.UseHttpsRedirection();

// app.UseAuthorization(); // Uncomment if you add authentication/authorization

app.MapControllers();

app.Run();