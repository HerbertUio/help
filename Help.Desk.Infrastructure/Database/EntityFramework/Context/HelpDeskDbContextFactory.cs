using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Context;

public class HelpDeskDbContextFactory : IDesignTimeDbContextFactory<HelpDeskDbContext>
{
    public HelpDeskDbContext CreateDbContext(string[] args)
    {
        // Obtener la ruta al directorio del proyecto de inicio (Help.Desk.Api)
        // Esto asume que la estructura de directorios es consistente:
        // Solution Root -> Help.Desk.Api
        // Solution Root -> Help.Desk.Infrastructure (donde está este archivo)
        // Navegamos dos niveles arriba desde Infrastructure para llegar a la raíz de la solución,
        // y luego bajamos a Api. Ajusta según tu estructura real si es diferente.
        
        // Intenta determinar la ruta base de forma más robusta
        // Asume que se ejecuta desde el directorio del proyecto Infrastructure
        string apiProjectPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Help.Desk.Api"));

        // Si lo anterior falla (ej. si se ejecuta desde la raíz), intenta otra ruta
        if (!Directory.Exists(apiProjectPath))
        {
            apiProjectPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "Help.Desk.Api"));
        }
        // Si aún falla, podrías necesitar una estrategia más avanzada o verificar dónde se ejecuta 'dotnet ef'
         if (!Directory.Exists(apiProjectPath))
        {
             // Como último recurso, asume que el directorio actual es la raíz de la solución
             // o el proyecto API directamente. Si no, lanza un error más claro.
             apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "Help.Desk.Api");
             if (!Directory.Exists(apiProjectPath)) {
                 apiProjectPath = Directory.GetCurrentDirectory(); // Asume que estamos ya en el directorio API
             }
        }
         Console.WriteLine($"Design-Time Factory attempting to use Base Path: {apiProjectPath}"); // Ayuda para depurar


        if (!Directory.Exists(apiProjectPath))
        {
             throw new DirectoryNotFoundException($"Could not find the API project directory. Tried paths relative to {AppContext.BaseDirectory} and {Directory.GetCurrentDirectory()}. Ensure you are running 'dotnet ef' from the solution root or specify the startup project correctly.");
         }


        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath) // Establece la base en el proyecto API
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Lee appsettings.json de la API
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true) // Lee appsettings.Development.json (o el del entorno)
            .AddEnvironmentVariables() // Opcional: permite sobreescribir con variables de entorno
            // .AddUserSecrets<HelpDeskDbContextFactory>() // Puedes añadir UserSecrets si los usas también en la API para la DB
            .Build();

        // Ahora intenta obtener la cadena de conexión desde esta configuración
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        Console.WriteLine($"Found Connection String: {!string.IsNullOrEmpty(connectionString)}"); // Ayuda para depurar


        if (string.IsNullOrEmpty(connectionString))
        {
            // Error más específico si no se encuentra
            throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no se encontró en los archivos appsettings.json o appsettings.Development.json del proyecto de inicio (Help.Desk.Api).");
        }

        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new HelpDeskDbContext(optionsBuilder.Options);
    }
}