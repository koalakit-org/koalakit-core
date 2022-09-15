using Koalakit.Sample.SimpleApi.Entities.DbProviders;
using Koalakit.Sample.SimpleApi.Services;
using KoalaKit.Extensions;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<WeatherForecastsService>();
builder.Services.AddKoalaKitCore(builder => builder.AddModules(typeof(SqlServerModule)));
EntityProvidersCollection.AddDbEntityProvider(typeof(ForecastSqLDbProvider).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();