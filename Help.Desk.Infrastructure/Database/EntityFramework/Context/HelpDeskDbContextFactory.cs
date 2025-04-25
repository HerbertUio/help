using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Context;

public class HelpDeskDbContextFactory: IDesignTimeDbContextFactory<HelpDeskDbContext>
{
    public HelpDeskDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<HelpDeskDbContextFactory>()
            .Build();
        string connectionString = configurationRoot.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula ni estar vacía.");
        }
        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new HelpDeskDbContext(optionsBuilder.Options);
    }
}