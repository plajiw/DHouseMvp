using Microsoft.EntityFrameworkCore;
using DHouseMvp.Infrastructure.Data;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(
                                "http://localhost:3000",
                                "http://localhost:5500",
                                "http://127.0.0.1:5500",
                                "http://localhost:8080",
                                "http://127.0.0.1:8080"
                                )
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); 
                      });
});


builder.Services.AddMvcCore().AddApiExplorer();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(conn));

builder.Services.AddScoped<IImovelService, ImovelService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DHouseMvp API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();
app.Run();