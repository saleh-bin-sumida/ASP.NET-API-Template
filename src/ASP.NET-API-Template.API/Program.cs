using ASP.NET_API_Template.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersServices();
builder.Services.AddOpenApiServices();
builder.Services.AddSwaggerServices();
builder.Services.AddEFServices(builder.Configuration);
builder.Services.AddCoreServices();
var app = builder.Build();

app.UseOpenApiAndScalar();
app.UseSwaggerTool();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RedirectToSwaggerMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
