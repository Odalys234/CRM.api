using CRM.API.Endpoints;
using CRM.API.Models.DAL;
using Microsoft.EntityFrameworkCore;
//Crea un nuevo constructor en la aplicacion web
var builder = WebApplication.CreateBuilder(args);
//agrega servicios para habilitar la generacion de documentacion de API

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configura y agrega un contexto de base de datos para entity framework core.
builder.Services.AddDbContext<CRMContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
);
//agrega una instancia de la clase CustomerDAL como un servicio para la inyeccion de dependencias.
builder.Services.AddScoped<CustomerDAL>();
//Construye la aplicacion web .

var app = builder.Build();
//agrega los puntos finales relacionados con los clientes a la aplicacion.
app.AddCustomerEndpoints();
//verifica si la aplicacion se esta ejecutando en un entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    //habilita el uso de swagger para la documentacion de la api.
    app.UseSwagger();
    app.UseSwaggerUI();
}
//agrega middleware para redirigir las solicitudes http a https.

app.UseHttpsRedirection();
//ejecuta la aplicacion.
app.Run();
