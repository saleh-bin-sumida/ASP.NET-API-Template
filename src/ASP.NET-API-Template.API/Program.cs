using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersServices();
builder.Services.AddOpenApiServices();
builder.Services.AddEFServices(builder.Configuration);
builder.Services.ConfigMapster();
builder.Host.UseSerilogLoggging();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseOpenApiAndScalar();
app.UseSwaggerTool();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RedirectToSwaggerMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
