using Koalakit.Sample.SimpleApi.ActionModels;
using Koalakit.Sample.SimpleApi.Entities.DbProviders;
using Koalakit.Sample.SimpleApi.Handlers;
using Koalakit.Sample.SimpleApi.Services;
using KoalaKit.Extensions;
using KoalaKit.Messaging;
using KoalaKit.Messaging.Queuing.Extensions;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.SqlServer;
using KoalaKit.Queuing.RabbitMq;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<WeatherForecastsService>();
builder.Services.AddTransient<IMessagingHandler<WeatherForecastsPublishingParameters>, WeatherForecastsHandler>();
var modules = new Type[] { typeof(SqlServerModule), typeof(RabbitMqModule) };
builder.Services.AddKoalaKitCore(builder => builder.AddModules(modules));
EntityProvidersCollection.AddDbEntityProvider(typeof(ForecastSqLDbProvider).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RunQueuingConsumers(typeof(WeatherForecastsPublishingParameters));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();