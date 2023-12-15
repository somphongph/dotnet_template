using System.Text.Json;
using API.Middleware;
using Domain;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Configuration.AddEnvironmentVariables("ASPNETCORE_");
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();


#region Service Register & Dependency Injection
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRequestCulture();

app.MapHealthChecks("/healthz");

app.Run();
