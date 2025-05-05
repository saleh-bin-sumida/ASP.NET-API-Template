namespace ASP.NET_API_Template.API.Startup;

public static class ControllersConfig
{
    public static void AddControllersServices(this IServiceCollection services)
    {
        // Register global filters and configure JSON options
        services.AddControllers()

            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })

            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(kvp => kvp.Value.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    var response = BaseResponse<object>
                    .ErrorResponse("One or more validation errors occurred.", errors);

                    return new BadRequestObjectResult(response)
                    {
                        ContentTypes = { "application/json" }
                    };
                };
            });
    }
}
