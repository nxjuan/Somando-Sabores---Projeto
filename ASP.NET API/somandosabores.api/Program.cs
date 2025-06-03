using domain.IServices;
using infra.DbContext;
using Microsoft.EntityFrameworkCore;
using somandosabores.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql((builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigin", 
    builder => {
        builder.WithOrigins("http://localhost:4200")
        .WithHeaders("Content-Type", "Authorization")
        // CORS header ‘Access-Control-Allow-Origin’
        .WithMethods("GET", "POST");});
});

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IReservaService, ReserveService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
