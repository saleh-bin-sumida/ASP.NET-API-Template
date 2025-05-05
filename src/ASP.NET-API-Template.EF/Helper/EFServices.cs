namespace ASP.NET_API_Template.EF.Helper;

public static class EFServices
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LocalConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException($"Connection string {connectionString} is not found.");

        return connectionString;
    }

    public static void AddEFServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(GetConnectionStringOrThrow(configuration));
            options.EnableSensitiveDataLogging();
        });
    }
}
