using Microsoft.EntityFrameworkCore;
using DHouseMvp.Infrastructure.Data;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection; // For Swagger XML Comments if used

// If MappingProfile is in a different assembly and not picked up by AppDomain scan,
// you might need: using DHouseMvp.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuração de Serviços (Injeção de Dependência)
builder.Services.AddMvcCore().AddApiExplorer();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(conn));

builder.Services.AddScoped<IImovelService, ImovelService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

// This should pick up MappingProfile if it's in any loaded assembly.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DHouseMvp API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configuração do Pipeline de Requisições HTTP
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
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DHouseMvp API V1");
        // c.RoutePrefix = string.Empty; // Descomente para acessar Swagger na raiz do site
    });
}
else
{
    app.UseExceptionHandler("/Error"); // Basic error handler
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers(); // Mapeia as rotas para os seus Controllers
app.Run();