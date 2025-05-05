
namespace ASP.NET_API_Template.API.Startup;

public static class SwaggerConfig
{
    public static void UseSwaggerTool(this WebApplication app)
    {
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/openapi/v1.json", "Heading here");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
    }
}
