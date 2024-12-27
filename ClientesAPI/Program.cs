////////using ClientesAPI.Data;
////////using Microsoft.EntityFrameworkCore;

////////var builder = WebApplication.CreateBuilder(args);

////////string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

////////if (string.IsNullOrEmpty(connectionString))
////////{
////////    throw new InvalidOperationException("La cadena de conexión no está configurada.");
////////}

////////builder.Services.AddDbContext<AppDbContext>(options =>
////////    options.UseSqlServer(connectionString));

////////// Agregar servicios para la interfaz de Swagger
////////builder.Services.AddEndpointsApiExplorer();  
////////builder.Services.AddSwaggerGen(); 

////////builder.Services.AddControllers();

////////var app = builder.Build();

////////if (app.Environment.IsDevelopment())
////////{
////////    app.UseSwagger(); 
////////    app.UseSwaggerUI();  
////////}

////////app.UseRouting();

////////app.MapControllers();

////////app.Run();

using ClientesAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión no está configurada.");
}

// Configuración de DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS para permitir solicitudes desde Angular
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Origen de tu app Angular
              .AllowAnyHeader()    // Permitir cualquier cabecera
              .AllowAnyMethod();   // Permitir cualquier método (GET, POST, etc.)
    });
});

// Agregar servicios para controladores
builder.Services.AddControllers();

var app = builder.Build();

// Usar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS antes de otras configuraciones
app.UseCors();

// Rutas de los controladores
app.UseRouting();

// Mapear controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
