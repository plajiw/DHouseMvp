using Microsoft.EntityFrameworkCore;
using DHouseMvp.Infrastructure.Data;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.Services;
using AutoMapper;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// → Habilita MVC Core + ApiExplorer (importante para controllers + Swagger)
builder.Services
    .AddMvcCore()
    .AddApiExplorer();

// Carrega connection string do appsettings.json
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// EF Core + SQLite
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlite(conn));

// Serviços da aplicação
builder.Services.AddScoped<IImovelService, ImovelService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Endpoints API Explorer (para minimal APIs, mas deixamos)
builder.Services.AddEndpointsApiExplorer();

// SwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DHouseMvp API", Version = "v1" });
    // se quiser XML comments, inclua aqui o IncludeXmlComments…
});

var app = builder.Build();

// Aplica migrações
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DHouseMvp API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
