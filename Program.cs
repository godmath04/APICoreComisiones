using APICoreComisiones.Application;
using APICoreComisiones.Data;
using APICoreComisiones.Domain.Commission;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === Infra: EF Core ===
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// === Dominio / Aplicación ===
builder.Services.AddScoped<ICommissionPolicy, FlatCommissionPolicy>();
builder.Services.AddScoped<ICommissionService, CommissionService>();

// === API ===
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
    });

// === Swagger ===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// === CORS para Angular local ===
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAngular",
        p => p.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// === Pipeline ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS antes de MapControllers
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
