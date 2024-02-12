var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias
builder.Services.AddControllersWithViews();

// Configura y agrega un cliente HTTP con nombre "CRMAPI"
builder.Services.AddHttpClient("CRMAPI", c =>
{
    // Configura la direcci�n base del cliente HTTP desde la configuraci�n
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);

    // Puedes configurar otras opciones del HttpClient aqu� seg�n sea necesario
});

var app = builder.Build(); // Crea una instancia de la aplicaci�n web

// Configura el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    // Maneja excepciones en caso de errores y redirige a la acci�n "Error" en el controlador "Home"
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirige las solicitudes HTTP a HTTPS
app.UseStaticFiles(); // Habilita el uso de archivos est�ticos como CSS, Javascript, im�genes, etc.
app.UseRouting(); // Configura el enrutamiento de solicitudes
app.UseAuthorization(); // Habilita la autorizaci�n para proteger rutas y acciones de controladores

// Mapea la ruta predeterminada de controlador y acci�n
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Inicia la aplicaci�n web
