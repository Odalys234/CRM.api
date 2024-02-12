var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias
builder.Services.AddControllersWithViews();

// Configura y agrega un cliente HTTP con nombre "CRMAPI"
builder.Services.AddHttpClient("CRMAPI", c =>
{
    // Configura la dirección base del cliente HTTP desde la configuración
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);

    // Puedes configurar otras opciones del HttpClient aquí según sea necesario
});

var app = builder.Build(); // Crea una instancia de la aplicación web

// Configura el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    // Maneja excepciones en caso de errores y redirige a la acción "Error" en el controlador "Home"
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirige las solicitudes HTTP a HTTPS
app.UseStaticFiles(); // Habilita el uso de archivos estáticos como CSS, Javascript, imágenes, etc.
app.UseRouting(); // Configura el enrutamiento de solicitudes
app.UseAuthorization(); // Habilita la autorización para proteger rutas y acciones de controladores

// Mapea la ruta predeterminada de controlador y acción
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Inicia la aplicación web
