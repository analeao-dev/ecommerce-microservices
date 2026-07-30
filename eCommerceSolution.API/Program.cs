using eCommerce.Core;
using eCommerce.Infrastructure;
using eCommerceSolution.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddCore();

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();