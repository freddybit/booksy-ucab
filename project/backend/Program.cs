

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<backend.services.BookService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy => policy
            .SetIsOriginAllowed(origin =>
            {
                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri)) return false;
                return uri.Host is "localhost" or "127.0.0.1";
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();

var app = builder.Build();

// Usar CORS antes de MapControllers
app.UseCors("AllowVue");

app.UseAuthorization();
app.MapControllers();
app.Run();